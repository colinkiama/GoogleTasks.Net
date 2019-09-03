using GoogleTasksNET.ModelBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleTasksNET.Model
{
    public class GTaskList:Notifier
    {
        private string _kind;
        [JsonProperty("kind")]
        public string Kind { get { return _kind; } set { _kind = value; NotifyPropertyChanged(); } }


        private string _id;
        [JsonProperty("id")]
        public string ID { get { return _id; } set { _id = value; NotifyPropertyChanged(); } }

        private string _etag;
        [JsonProperty("etag")]
        public string ETag { get { return _etag; } set { _etag = value; NotifyPropertyChanged(); } }

        private string _title;
        [JsonProperty("title")]
        public string Title { get { return _title; } set { _title = value; NotifyPropertyChanged(); } }

        private DateTime _updated;
        [JsonProperty("updated")]
        public DateTime Updated { get { return _updated; } set { _updated = value; NotifyPropertyChanged(); } }

        private string _selfLink;
        [JsonProperty("selfLink")]
        public string SelfLink { get { return _selfLink; } set { _selfLink = value; NotifyPropertyChanged(); } }
    }
}
