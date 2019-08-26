using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleTasksNET
{
    public class GTaskList
    {
        [JsonProperty("kind")]
        public string kind { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("etag")]
        public string etag { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("updated")]
        public DateTime updated { get; set; }

        [JsonProperty("selfLink")]
        public string selfLink { get; set; }
    }
}
