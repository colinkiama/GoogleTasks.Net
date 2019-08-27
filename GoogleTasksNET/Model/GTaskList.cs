using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleTasksNET.Model
{
    public class GTaskList
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("etag")]
        public string ETag { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        [JsonProperty("selfLink")]
        public string SelfLink { get; set; }
    }
}
