using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleTasksNET
{
    public class GTaskList
    {
        public string kind { get; set; }
        public string id { get; set; }
        public string etag { get; set; }
        public string title { get; set; }
        public DateTime updated { get; set; }
        public string selfLink { get; set; }
    }
}
