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

        public Token()
        {

        }

        public Token(string accessToken, string refreshToken, uint expiresIn)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpirationDateTimeUTC = CalculateExpirationTimeFromCurrentTime(expiresIn);
        }

        private DateTime CalculateExpirationTimeFromCurrentTime(uint expiresIn)
        {
            ExpirationDateTimeUTC = DateTime.UtcNow.Subtract(new TimeSpan(0, 0, (int)expiresIn));
            return ExpirationDateTimeUTC;
        }
    }
}
