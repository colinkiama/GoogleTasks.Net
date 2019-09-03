using GoogleTasksNET.ModelBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleTasksNET.Model
{
    public class GTask : Notifier
    {
        private string _kind;

        [JsonProperty("kind")]
        public string Kind
        {
            get { return _kind; }
            set
            {
                _kind = value;
                NotifyPropertyChanged();
            }
        }




        private string _id;
        [JsonProperty("id")]
        public string ID
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }

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


        private string _parent;
        [JsonProperty("parent")]
        public string Parent { get { return _parent; } set { _parent = value; NotifyPropertyChanged(); } }


        private string _position;
        [JsonProperty("position")]
        public string Position { get { return _position; } set { _position = value; NotifyPropertyChanged(); } }


        private string _notes;
        [JsonProperty("notes")]
        public string Notes { get { return _notes; } set { NotifyPropertyChanged(); } }


        private string _status;
        /// <summary>
        /// Either "needsAction" or "completed"
        /// </summary>
        [JsonProperty("status")]
        public string Status { get { return _status; } set { _status = value; NotifyPropertyChanged(); } }


        private DateTime _due;
        [JsonProperty("due")]
        public DateTime Due { get { return _due; } set {_due = value; NotifyPropertyChanged(); } }

        private DateTime _completed;
        [JsonProperty("completed")]
        public DateTime Completed { get { return _completed; } set {_completed = value; NotifyPropertyChanged(); } }

        private bool _deleted;
        [JsonProperty("deleted")]
        public bool Deleted { get { return _deleted; } set { _deleted = value; NotifyPropertyChanged(); } }


        private bool _hidden;
        /// <summary>
        /// Flag indicating whether the task is hidden. 
        /// This is the case if the task had been marked completed when the 
        /// task list was last cleared. 
        /// The default is False. This field is read-only.
        /// </summary>
        [JsonProperty("hidden")]
        public bool Hidden { get { return _hidden; } set { _hidden = value; NotifyPropertyChanged(); } }


        private List<Link> _links;
        /// <summary>
        /// Collection of links. This collection is read-only.
        /// </summary>
        [JsonProperty("links")]
        public List<Link> Links { get { return _links; } set { _links = value; NotifyPropertyChanged(); } }
    }

    public class Link:Notifier
    {
        private string _linkType;
        [JsonProperty("type")]
        public string LinkType { get { return _linkType; } set { _linkType = value; NotifyPropertyChanged(); } }

        private string _description;
        [JsonProperty("description")]
        public string Description { get { return _description; } set { _description = value; NotifyPropertyChanged(); } }

        private string _linkContent;
        [JsonProperty("link")]
        public string LinkContent { get { return _linkContent; } set { _linkContent = value; NotifyPropertyChanged(); } }
    }

}
