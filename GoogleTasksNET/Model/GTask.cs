using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleTasksNET.Model
{
    public class GTask
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

        [JsonProperty("parent")]
        public string Parent { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        /// <summary>
        /// Either "needsAction" or "completed"
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("due")]
        public DateTime Due { get; set; }

        [JsonProperty("completed")]
        public DateTime Completed { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        /// <summary>
        /// Flag indicating whether the task is hidden. 
        /// This is the case if the task had been marked completed when the 
        /// task list was last cleared. 
        /// The default is False. This field is read-only.
        /// </summary>
        [JsonProperty("hidden")]
        public bool Hidden { get; set; }


        /// <summary>
        /// Collection of links. This collection is read-only.
        /// </summary>
        [JsonProperty("links")]
        public List<Link> Links { get; set; }
    }

    public class Link
    {
        [JsonProperty("type")]
        public string LinkType { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("link")]
        public string LinkContent { get; set; }
    }

}
