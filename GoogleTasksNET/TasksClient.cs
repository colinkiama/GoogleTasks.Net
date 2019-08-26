using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GoogleTasksNET
{
    public class TasksClient
    {
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public Token ClientToken { get; set; }

        HttpClient _client = new HttpClient();

        public TasksClient(string clientID, string clientSecret, Token clientToken)
        {
            ClientID = clientID;
            ClientSecret = clientSecret;
            ClientToken = clientToken;
        }


        public async Task<ListResult<GTaskList>> GetTaskListsAsync(string pageToken = null, ulong maxResults = 20)
        {
            ListResult<GTaskList> result = null;

            string requestString = $"https://www.googleapis.com/tasks/v1/users/@me/lists";

            //string requestString = $"https://www.googleapis.com/tasks/v1/users/@me/lists" +
            //    $"?maxResults={maxResults}&pageToken={pageToken}";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestString);
            request.Headers.Add("Authorization", $"{ClientToken.GrantType} {ClientToken.AccessToken}");

            var responseMessage = await _client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonReturned = await responseMessage.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<ListResult<GTaskList>>(jsonReturned);
            }
            
            return result;
        }
    }
}
