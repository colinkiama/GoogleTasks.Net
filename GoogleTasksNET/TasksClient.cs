using GoogleTasksNET.Helpers;
using GoogleTasksNET.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
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

        const string JsonMediaType = "application/json";
        public TasksClient(string clientID, string clientSecret, Token clientToken)
        {
            ClientID = clientID;
            ClientSecret = clientSecret;
            ClientToken = clientToken;
        }


        // TaskList Methods

        public async Task<ListResult<GTaskList>> GetTaskListsAsync(string pageToken = null, ulong maxResults = 20)
        {
            ListResult<GTaskList> result = null;

            StringBuilder requestBuilder = new StringBuilder();
            requestBuilder.Append("https://www.googleapis.com/tasks/v1/users/@me/lists");

            Dictionary<string, object> queries = new Dictionary<string, object>();

            if (pageToken != null)
            {
                queries.Add("pageToken", pageToken);
            }

            queries.Add("maxResults", maxResults);

            RequestHelper.AddQueriesToRequest(requestBuilder, queries);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestBuilder.ToString());
            RequestHelper.AddAuthorizationHeader(request, ClientToken);

            var responseMessage = await _client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonReturned = await responseMessage.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<ListResult<GTaskList>>(jsonReturned);
            }

            return result;
        }

        public async Task<GTaskList> GetTaskListAsync(string taskListID)
        {
            GTaskList result = null;
            string requestUrl = $"https://www.googleapis.com/tasks/v1/users/@me/lists/{taskListID}";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            RequestHelper.AddAuthorizationHeader(request, ClientToken);

            var responseMessage = await _client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonReturned = await responseMessage.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<GTaskList>(jsonReturned);
            }

            return result;
        }

        public async Task<GTaskList> AddTaskListAsync(GTaskList listToAdd)
        {
            GTaskList result = null;

            string requestUrl = "https://www.googleapis.com/tasks/v1/users/@me/lists";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            RequestHelper.AddAuthorizationHeader(request, ClientToken);

            var listJson = new StringContent(JsonConvert.SerializeObject(listToAdd), Encoding.UTF8,
                JsonMediaType);

            request.Content = listJson;
            var responseMessage = await _client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonReturned = await responseMessage.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<GTaskList>(jsonReturned);
            }

            return result;
        }

        public async Task<GTaskList> UpdateTaskListAsync(GTaskList updatedList)
        {
            GTaskList result = null;

            string requestUrl = $"https://www.googleapis.com/tasks/v1/users/@me/lists/{updatedList.id}";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, requestUrl);
            RequestHelper.AddAuthorizationHeader(request, ClientToken);

            var listJson = new StringContent(JsonConvert.SerializeObject(updatedList), Encoding.UTF8,
                JsonMediaType);

            request.Content = listJson;
            var responseMessage = await _client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonReturned = await responseMessage.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<GTaskList>(jsonReturned);
            }

            return result;
        }

        public async Task<bool> DeleteTaskListAsync(string taskListID)
        {
            bool wasTaskListDeleted = false;

            string requestUrl = $"https://www.googleapis.com/tasks/v1/users/@me/lists/{taskListID}";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, requestUrl);
            RequestHelper.AddAuthorizationHeader(request, ClientToken);

            var responseMessage = await _client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                wasTaskListDeleted = true;
            }

            return wasTaskListDeleted;
        }

        // Task Methods

        public async Task<ListResult<GTask>> GetTasksAsync(string taskListID, ulong maxResults = 20, bool showCompleted = true,
            bool showDeleted = false, bool showHidden = false)
        {
            ListResult<GTask> result = null;

            StringBuilder requestBuilder = new StringBuilder();
            requestBuilder.Append($"https://www.googleapis.com/tasks/v1/lists/{taskListID}/tasks");

            Dictionary<string, object> queries = new Dictionary<string, object>();

            queries.Add("maxResults", maxResults);
            queries.Add("showCompleted", showCompleted);
            queries.Add("showDeleted", showDeleted);
            queries.Add("showHidden", showHidden);

            RequestHelper.AddQueriesToRequest(requestBuilder, queries);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestBuilder.ToString());
            RequestHelper.AddAuthorizationHeader(request, ClientToken);

            var responseMessage = await _client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonReturned = await responseMessage.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<ListResult<GTask>>(jsonReturned);
            }

            return result;
        }

        public async Task<ListResult<GTask>> GetTasksAsync(string taskListID, TasksQuery query)
        {
            ListResult<GTask> result = null;

            StringBuilder requestBuilder = new StringBuilder();
            requestBuilder.Append($"https://www.googleapis.com/tasks/v1/lists/{taskListID}/tasks");

            Dictionary<string, object> queries = new Dictionary<string, object>();

            queries.Add("maxResults", query.MaxResults);
            queries.Add("showCompleted", query.ShowCompleted);
            queries.Add("showDeleted", query.ShowDeleted);
            queries.Add("showHidden", query.ShowHidden);

            QueryHelper.TryAddToQueries(query, queries);

            RequestHelper.AddQueriesToRequest(requestBuilder, queries);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestBuilder.ToString());
            RequestHelper.AddAuthorizationHeader(request, ClientToken);

            var responseMessage = await _client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonReturned = await responseMessage.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<ListResult<GTask>>(jsonReturned);
            }

            return result;

        }

        public async Task<GTask> GetTaskAsync(string taskID, string taskListID)
        {
            GTask result = null;
            string requestUrl = $"https://www.googleapis.com/tasks/v1/lists/{taskListID}/tasks/{taskID}";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            RequestHelper.AddAuthorizationHeader(request, ClientToken);

            var responseMessage = await _client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonReturned = await responseMessage.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<GTask>(jsonReturned);
            }

            return result;
        }


        public async Task<GTask> AddTaskAsync(string taskListID, GTask taskToAdd)
        {
            GTask result = null;

            string requestUrl = $"https://www.googleapis.com/tasks/v1/lists/{taskListID}/tasks";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            RequestHelper.AddAuthorizationHeader(request, ClientToken);

            var listJson = new StringContent(JsonConvert.SerializeObject(taskToAdd), Encoding.UTF8,
                JsonMediaType);

            request.Content = listJson;
            var responseMessage = await _client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonReturned = await responseMessage.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<GTask>(jsonReturned);
            }

            return result;
        }

        public async Task<GTask> UpdateTaskAsync(string taskListID, GTask updatedTask)
        {
            GTask result = null;

            string requestUrl = $"https://www.googleapis.com/tasks/v1/lists/{taskListID}/tasks/{updatedTask.id}";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, requestUrl);
            RequestHelper.AddAuthorizationHeader(request, ClientToken);

            var listJson = new StringContent(JsonConvert.SerializeObject(updatedTask), Encoding.UTF8,
                JsonMediaType);

            request.Content = listJson;
            var responseMessage = await _client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonReturned = await responseMessage.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<GTask>(jsonReturned);
            }

            return result;
        }

        public async Task<bool> DeleteTaskAsync(string taskListID, string taskID)
        {
            bool wasTaskDeleted = false;

            string requestUrl = $"https://www.googleapis.com/tasks/v1/lists/{taskListID}/tasks/{taskID}";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, requestUrl);
            RequestHelper.AddAuthorizationHeader(request, ClientToken);

            var responseMessage = await _client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                wasTaskDeleted = true;
            }

            return wasTaskDeleted;
        }

        public async Task<bool> ClearTaskListAsync(string taskListID)
        {
            bool wasTaskCleared = false;

            string requestUrl = $"https://www.googleapis.com/tasks/v1/lists/{taskListID}/clear";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            RequestHelper.AddAuthorizationHeader(request, ClientToken);

            var responseMessage = await _client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                wasTaskCleared = true;
            }

            return wasTaskCleared;
        }


        public async Task<GTask> MoveTaskAsync(string taskID, string taskListID, string parentID = null, string previousSiblingID = null)
        {
            GTask result = null;

            StringBuilder requestBuilder = new StringBuilder();
            requestBuilder.Append($"https://www.googleapis.com/tasks/v1/lists/{taskListID}/tasks/{taskID}/move");

            Dictionary<string, object> queries = new Dictionary<string, object>();

            if (parentID != null)
            {
                queries.Add("parent", parentID);
            }

            if (previousSiblingID != null)
            {
                queries.Add("previous", previousSiblingID);
            }

            RequestHelper.AddQueriesToRequest(requestBuilder, queries);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestBuilder.ToString());
            RequestHelper.AddAuthorizationHeader(request, ClientToken);

            var responseMessage = await _client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonReturned = await responseMessage.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<GTask>(jsonReturned);
            }

            return result;
        }

    }
}
