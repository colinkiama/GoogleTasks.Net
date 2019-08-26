using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleTasksNET
{
    public class ListResult<T>
    {
        public string kind { get; set; }

        public string etag { get; set; }

        public string nextPageToken { get; set; }

        public T[] items { get; set; }
    }
}
