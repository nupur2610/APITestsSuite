/*using System;
using System.Threading.Tasks;
using Flurl.Http;
using RuleEngineAPITests.Model;


namespace RuleEngineAPITests.Helper
{
    public class EnterpriseToken : TokenGenIml
    {
        private static EnterpriseTokenResponseModel _token;
        private static EnterpriseToken entToken = null;
        public  AuthorizationsType AuthorizationsType => AuthorizationsType.EnterpriseApi;

        

        public static EnterpriseToken getInstance()
        {
            
        }
        

        public async Task CheckExpiry()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            if ((_token==null) || (_token.ExpiresOn-secondsSinceEpoch)<40)
            {
                await  GenerateEnterpriseApiAuth();

            }
            
        }

        private EnterpriseToken()
        {
        }


        //generating API token
        public  async  Task<EnterpriseTokenResponseModel>  GenerateEnterpriseApiAuth()
        {
          
            object request = new EnterpriseTokenRequest();
            try
            {
                _token =
                    await "https://login.microsoftonline.com/c8551ca9-adcd-4238-8af6-334e28b74652/oauth2/token"
                        .PostUrlEncodedAsync(request)
                        .ReceiveJson<EnterpriseTokenResponseModel>();
            }
            catch (FlurlHttpException ex)
            {
                throw new Exception($"Error getting the Token: {ex.ToString()}");
            }

            return _token;

        }
    }
}*/