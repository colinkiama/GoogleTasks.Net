using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleTasksNET
{
    public class TasksQuery
    {
        public DateTime CompletedMax { get; set; } = DateTime.MinValue;
        public DateTime CompletedMin { get; set; } = DateTime.MinValue;
        public DateTime DueMax { get; set; } = DateTime.MinValue;
        public DateTime DueMin { get; set; } = DateTime.MinValue;
        public ulong MaxResults { get; set; } = 20;
        public string PageToken { get; set; } = null;
        public bool ShowCompleted { get; set; } = true;
        public bool ShowDeleted { get; set; } = false;
        public bool ShowHidden { get; set; } = false;
        public DateTime UpadtedMin { get; set; } = DateTime.MinValue;

        public TasksQuery()
        {

        }

        public TasksQuery(ulong maxResults = 20, string pageToken = null, bool showCompleted = true)
        {
            MaxResults = maxResults;
            PageToken = pageToken;
            ShowCompleted = showCompleted;
        }
    }
}
