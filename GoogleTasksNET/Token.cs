using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleTasksNET
{
    public class Token
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpirationDateTimeUTC { get; set; }
        public string GrantType { get; private set; } = "Bearer";
    }
}
