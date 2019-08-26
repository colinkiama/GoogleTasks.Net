using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleTasksNET
{
    public class ListResult<T>
    {
        [JsonProperty("kind")]
        public string kind { get; set; }

        [JsonProperty("etag")]
        public string etag { get; set; }

        [JsonProperty("nextPageToken")]
        public string nextPageToken { get; set; }

        [JsonProperty("items")]
        public List<T> items { get; set; }
    }
}
