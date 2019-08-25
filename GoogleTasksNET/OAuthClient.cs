using System;
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

namespace GoogleTasksNET
{
    public class OAuthClient
    {
        const string authorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
        const string tokenEndpoint = "https://www.googleapis.com/oauth2/v4/token";
        const string code_challenge_method = "S256";

        public string ClientID { get; set; }
        public string ClientSecret { get; set; }



        public async Task<string> AuthenticateAsync(AuthMethod authMethod)
        {
            string state = randomDataBase64url(32);
            string code_verifier = randomDataBase64url(32);
            string code_challenge = base64urlencodeNoPadding(sha256(code_verifier));

            string redirectURI = null;
            string authorizationRequest = null;
            switch (authMethod)
            {
                case AuthMethod.CustomURIScheme:
                    break;
                case AuthMethod.Loopback:
                    redirectURI = string.Format("http://{0}:{1}/", IPAddress.Loopback, GetRandomUnusedPort());
                    authorizationRequest = string.Format("{0}?response_type=code&scope=openid%20profile&redirect_uri={1}&client_id={2}&state={3}&code_challenge={4}&code_challenge_method={5}",
                    authorizationEndpoint,
                    System.Uri.EscapeDataString(redirectURI),
                    ClientID,
                    state,
                    code_challenge,
                    code_challenge_method);
                    break;

            }

            return authorizationRequest;
        }

        public static string randomDataBase64url(uint length)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[length];
            rng.GetBytes(bytes);
            return base64urlencodeNoPadding(bytes);
        }

        public static string base64urlencodeNoPadding(byte[] buffer)
        {
            string base64 = Convert.ToBase64String(buffer);

            // Converts base64 to base64url.
            base64 = base64.Replace("+", "-");
            base64 = base64.Replace("/", "_");
            // Strips padding.
            base64 = base64.Replace("=", "");

            return base64;
        }

        public static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        public static byte[] sha256(string inputStirng)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(inputStirng);
            SHA256Managed sha256 = new SHA256Managed();
            return sha256.ComputeHash(bytes);
        }
    }


}
