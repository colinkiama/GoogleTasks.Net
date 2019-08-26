using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GoogleTasksNET.Helpers
{
    public static class RequestHelper
    {
        internal static void AddAuthorizationHeader(HttpRequestMessage request, Token token)
        {
            request.Headers.Add("Authorization", $"{token.GrantType} {token.AccessToken}");
        }

        internal static void AddQueriesToRequest(StringBuilder requestBuilder, Dictionary<string, object> queries)
        {
            if (queries.Count > 0)
            {
                requestBuilder.Append('?');

                foreach (var query in queries)
                {
                    requestBuilder.Append($"{query.Key}=");
                    requestBuilder.Append($"{query.Value}&");
                }

                // Removes last '&'
                requestBuilder.Remove(requestBuilder.Length - 1, 1);
            }
        }

    }
}
