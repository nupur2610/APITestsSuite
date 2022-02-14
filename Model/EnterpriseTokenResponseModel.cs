using Newtonsoft.Json;

namespace ReferralOrchAPITest.Model
{
    public class EnterpriseTokenResponseModel
    {
        
            [JsonProperty("token_type")]
            public string TokenType { get; set; }

            [JsonProperty("expires_in")]
            public long ExpiresIn { get; set; }

            [JsonProperty("ext_expires_in")]
            public long ExtExpiresIn { get; set; }

            [JsonProperty("expires_on")]
            public long ExpiresOn { get; set; }

            [JsonProperty("not_before")]
            public long NotBefore { get; set; }

            [JsonProperty("resource")]
            public string Resource { get; set; }

            [JsonProperty("access_token")]
            public string AccessToken { get; set; }
        
    }
}