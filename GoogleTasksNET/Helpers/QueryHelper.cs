using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace GoogleTasksNET.Helpers
{
    public static class QueryHelper
    {
        internal static void TryAddToQueries(TasksQuery query, Dictionary<string, object> queries)
        {
            if (!CheckIfMinDateTime(query.CompletedMax))
            {
                queries.Add("completedMax", GenerateRFC3339String(query.CompletedMax));
            }

            if (!CheckIfMinDateTime(query.CompletedMin))
            {
                queries.Add("completedMin", GenerateRFC3339String(query.CompletedMin));
            }

            if (!CheckIfMinDateTime(query.DueMax))
            {
                queries.Add("dueMax", GenerateRFC3339String(query.DueMax));
            }


            if (!CheckIfMinDateTime(query.DueMin))
            {
                queries.Add("dueMin", GenerateRFC3339String(query.DueMin));
            }

            if (!CheckIfMinDateTime(query.UpadtedMin))
            {
                queries.Add("upadtedMin", GenerateRFC3339String(query.UpadtedMin));
            }

            if (query.PageToken != null)
            {
                queries.Add("pageToken", query.PageToken);
            }
        }

        private static string GenerateRFC3339String(DateTime dateTime)
        {
            return XmlConvert.ToString(dateTime, XmlDateTimeSerializationMode.Utc);
        }

        private static bool CheckIfMinDateTime(DateTime completedMax)
        {
            return completedMax == DateTime.MinValue;
        }
    }
}
