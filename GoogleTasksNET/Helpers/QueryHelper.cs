using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleTasksNET.Helpers
{
    public static class QueryHelper
    {
        internal static void TryAddToQueries(TasksQuery query, Dictionary<string, object> queries)
        {
            if (CheckIfMinDateTime(query.CompletedMax))
            {
                queries.Add("completedMax", query.CompletedMax);
            }

            if (CheckIfMinDateTime(query.CompletedMin))
            {
                queries.Add("completedMin", query.CompletedMin);
            }

            if (CheckIfMinDateTime(query.DueMax))
            {
                queries.Add("dueMax", query.DueMax);
            }


            if (CheckIfMinDateTime(query.DueMin))
            {
                queries.Add("dueMin", query.DueMin);
            }

            if (CheckIfMinDateTime(query.UpadtedMin))
            {
                queries.Add("upadtedMin", query.UpadtedMin);
            }

            if (query.PageToken != null)
            {
                queries.Add("pageToken", query.PageToken);
            }
        }

        private static bool CheckIfMinDateTime(DateTime completedMax)
        {
            return completedMax == DateTime.MinValue;
        }
    }
}
