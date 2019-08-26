using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleTasksNET.Model
{
    public class GTask
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

        [JsonProperty("parent")]
        public string parent { get; set; }

        [JsonProperty("position")]
        public string position { get; set; }

        [JsonProperty("notes")]
        public string notes { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("due")]
        public DateTime due { get; set; }

        [JsonProperty("completed")]
        public DateTime completed { get; set; }

        [JsonProperty("deleted")]
        public bool deleted { get; set; }

        [JsonProperty("hidden")]
        public bool hidden { get; set; }

        [JsonProperty("links")]
        public List<Link> links { get; set; }
    }

    public class Link
    {
        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("link")]
        public string link { get; set; }
    }

}
