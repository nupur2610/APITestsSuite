using System;
using System.Threading.Tasks;
using Flurl.Http;
using ReferralOrchAPITest.Model;

namespace ReferralOrchAPITest.Helper
{
    public  enum AuthorizationType
    {
        EnterpriseApi
    }

    public class Authorization
    { 
        AuthorizationType authType;
        private static EnterpriseTokenResponseModel _token;
        

        public Authorization(AuthorizationType authType)
        {
            this.authType = authType;

        }

        
        public async Task<EnterpriseTokenResponseModel> GetAuthorized()
        {
            switch (authType)
            {
                case AuthorizationType.EnterpriseApi:
                   await CheckExpiry();
                   break;
            }

            return _token;
        }

        //use this to see new token needs to be generated or not
        private async Task<EnterpriseTokenResponseModel> CheckExpiry()
        {
            var t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            var secondsSinceEpoch = (int)t.TotalSeconds;
            if ((_token==null) || (_token.ExpiresOn-secondsSinceEpoch)<40)
            {
               await  GenerateEnterpriseApiAuth();

            }

            return _token;
        }
        
     
        //generating API token
        //"https://login.microsoftonline.com/c8551ca9-adcd-4238-8af6-334e28b74652/oauth2/token"
        private static async Task<EnterpriseTokenResponseModel> GenerateEnterpriseApiAuth()
        {
          
            object request = new EnterpriseTokenRequest();
            try
            {
               var env="QA";
               var url = "";
               switch (env)
               {
                   case (TestEnvironment.QA):
                        url = "https://login.microsoftonline.com/c8551ca9-adcd-4238-8af6-334e28b74652/oauth2/token";
                       break;

               }

            
                _token =
                    await url
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
    }

