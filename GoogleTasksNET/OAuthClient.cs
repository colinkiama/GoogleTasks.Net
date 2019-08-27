﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;

namespace GoogleTasksNET
{
    public class OAuthClient
    {
        const string authorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
        const string tokenEndpoint = "https://www.googleapis.com/oauth2/v4/token";
        const string code_challenge_method = "S256";



        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectURI { get; set; }

        public string CodeVerifier { get; set; }

        string state = null;

        public OAuthClient()
        {

        }

        public OAuthClient(string clientID, string clientSecret)
        {
            ClientID = clientID;
            ClientSecret = clientSecret;
        }

        public OAuthClient(string clientID, string clientSecret, string redirectURI)
        {
            ClientID = clientID;
            ClientSecret = clientSecret;
            RedirectURI = redirectURI;
        }


        /// <summary>
        /// Launch the url from this returned from this method in a browser to
        /// to allow the user to login
        /// </summary>
        /// <returns></returns>
        public string GenerateAuthorizationURL()
        {
            state = randomDataBase64url(32);
            CodeVerifier = randomDataBase64url(32);
            string code_challenge = base64urlencodeNoPadding(sha256(CodeVerifier));


            string authorizationRequest = string.Format("{0}?response_type=code&scope=openid%20profile%20https://www.googleapis.com/auth/tasks&redirect_uri={1}&client_id={2}&state={3}&code_challenge={4}&code_challenge_method={5}",
            authorizationEndpoint,
            System.Uri.EscapeDataString(RedirectURI),
            ClientID,
            state,
            code_challenge,
            code_challenge_method);



            return authorizationRequest;
        }


        /// <summary>
        /// Should use this when using the loopback method with a HttpListener
        /// to listen for a redirect, to obtain your token.
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public async Task<Token> FinishOAuthAsync(NameValueCollection queryString)
        {
            Token tokenToReturn = null;
            if (queryString.Get("error") == null)
            {
                var code = queryString.Get("code");
                var incoming_state = queryString.Get("state");

                if (incoming_state != state)
                {
                    Debug.WriteLine($"Received request with invalid state ({incoming_state})");
                }
                else
                {
                    Debug.WriteLine($"Authorization code: {code}");
                }

                tokenToReturn = await GenerateTokenFromCodeExchangeAsync(code, CodeVerifier, RedirectURI);
            }

            return tokenToReturn;
        }

        /// <summary>
        /// Should use this to obtain a token when using other methods 
        /// e.g Custom URI.
        /// </summary>
        /// <param name="oauthResponseContent"></param>
        /// <returns></returns>
        public async Task<Token> FinishOAuthAsync(string code, string incomingState)
        {
            if (incomingState != state)
            {
                Debug.WriteLine($"Received request with invalid state ({incomingState})");
            }
            else
            {
                Debug.WriteLine($"Authorization code: {code}");
            }

            Token tokenToReturn = await GenerateTokenFromCodeExchangeAsync(code, CodeVerifier, RedirectURI);
            return tokenToReturn;
        }

        /// <summary>
        /// Whenever your token expires or is about to expire, you should use this to obtain 
        /// a new token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task<Token> RefreshTokenAsync(string refreshToken)
        {
            const string grantType = "refresh_token";
            Token tokenToReturn = null;

            // builds the  request
            string tokenRequestURI = tokenEndpoint;
            string tokenRequestBody = $"client_id={ClientID}&client_secret={ClientSecret}&refresh_token={refreshToken}&grant_type={grantType}";

            // sends the request
            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(tokenRequestURI);
            tokenRequest.Method = "POST";
            tokenRequest.ContentType = "application/x-www-form-urlencoded";


            byte[] _byteVersion = Encoding.ASCII.GetBytes(tokenRequestBody);
            tokenRequest.ContentLength = _byteVersion.Length;
            Stream stream = tokenRequest.GetRequestStream();
            await stream.WriteAsync(_byteVersion, 0, _byteVersion.Length);
            stream.Close();

            try
            {
                // gets the response
                WebResponse tokenResponse = await tokenRequest.GetResponseAsync();
                using (StreamReader reader = new StreamReader(tokenResponse.GetResponseStream()))
                {
                    // reads response body
                    string responseText = await reader.ReadToEndAsync();
                    Console.WriteLine(responseText);

                    // converts to dictionary
                    Dictionary<string, string> tokenEndpointDecoded = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseText);

                    string access_token = tokenEndpointDecoded["access_token"];
                    uint expires_in = uint.Parse(tokenEndpointDecoded["expires_in"]);

                    tokenToReturn = new Token(access_token, refreshToken, expires_in);
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        Debug.WriteLine("HTTP: " + response.StatusCode);
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            // reads response body
                            string responseText = await reader.ReadToEndAsync();
                            Debug.WriteLine(responseText);
                        }
                    }

                }
            }

            return tokenToReturn;


        }

        private async Task<Token> GenerateTokenFromCodeExchangeAsync(string code, string codeVerifier, string redirectURI)
        {
            Token tokenToReturn = null;

            // builds the  request
            string tokenRequestURI = tokenEndpoint;
            string tokenRequestBody = string.Format("code={0}&redirect_uri={1}&client_id={2}&code_verifier={3}&client_secret={4}&scope=&grant_type=authorization_code",
                code,
                System.Uri.EscapeDataString(redirectURI),
                ClientID,
                CodeVerifier,
                ClientSecret
                );

            // sends the request
            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(tokenRequestURI);
            tokenRequest.Method = "POST";
            tokenRequest.ContentType = "application/x-www-form-urlencoded";
            tokenRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            byte[] _byteVersion = Encoding.ASCII.GetBytes(tokenRequestBody);
            tokenRequest.ContentLength = _byteVersion.Length;
            Stream stream = tokenRequest.GetRequestStream();
            await stream.WriteAsync(_byteVersion, 0, _byteVersion.Length);
            stream.Close();

            try
            {
                // gets the response
                WebResponse tokenResponse = await tokenRequest.GetResponseAsync();
                using (StreamReader reader = new StreamReader(tokenResponse.GetResponseStream()))
                {
                    // reads response body
                    string responseText = await reader.ReadToEndAsync();
                    Console.WriteLine(responseText);

                    // converts to dictionary
                    Dictionary<string, string> tokenEndpointDecoded = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseText);

                    string access_token = tokenEndpointDecoded["access_token"];
                    string refresh_token = tokenEndpointDecoded["refresh_token"];
                    uint expires_in = uint.Parse(tokenEndpointDecoded["expires_in"]);

                    tokenToReturn = new Token(access_token, refresh_token, expires_in);
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        Debug.WriteLine("HTTP: " + response.StatusCode);
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            // reads response body
                            string responseText = await reader.ReadToEndAsync();
                            Debug.WriteLine(responseText);
                        }
                    }

                }
            }

            return tokenToReturn;
        }

        private string randomDataBase64url(uint length)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[length];
            rng.GetBytes(bytes);
            return base64urlencodeNoPadding(bytes);
        }

        private string base64urlencodeNoPadding(byte[] buffer)
        {
            string base64 = Convert.ToBase64String(buffer);

            // Converts base64 to base64url.
            base64 = base64.Replace("+", "-");
            base64 = base64.Replace("/", "_");
            // Strips padding.
            base64 = base64.Replace("=", "");

            return base64;
        }



        private static byte[] sha256(string inputStirng)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(inputStirng);
            SHA256Managed sha256 = new SHA256Managed();
            return sha256.ComputeHash(bytes);
        }
    }


}
