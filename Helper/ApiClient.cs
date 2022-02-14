using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Newtonsoft.Json.Linq;
using Polly;

namespace ReferralOrchAPITest.Helper
        {

            public class ApiClient
        {
            private readonly HttpClient _client;

            public ApiClient(HttpClient client)
            {
                _client = client;
            }

            public async Task<HttpResponseMessage>  Send(JToken requestConfig, Dictionary<string, string> saveValue)
            {
                var requestMethod = requestConfig["method"].ToString();

                var baseUrl = ReadBaseUrl(requestConfig);

                var requestUrl = baseUrl + requestConfig["url"].ToString();
                foreach (var entry in saveValue)
                {
                    if (requestUrl.Contains("$$" + entry.Key))
                        requestUrl = requestUrl.Replace("$$" + entry.Key, entry.Value);
                }

                await AddRequestHeaders(requestConfig["headers"]);
                if (requestConfig["pollUntil"] != null)
                {
                    var pollUntil = requestConfig["pollUntil"];
                    return  await  SendRequest(requestMethod, requestUrl, requestConfig, pollUntil);
                    
                }
                return   await  SendRequest(requestMethod, requestUrl, requestConfig, "");
             
                
            }


            private async Task<HttpResponseMessage> SendRequest(string requestMethod, string requestUrl,
                JToken requestConfig, JToken pollUntill)
            {
                var retryPolicy = Policy
                    .HandleResult<HttpResponseMessage>(message =>
                        !checkValue(pollUntill, message.Content.ReadAsStringAsync().Result.ToString()))
                    .WaitAndRetryAsync(100, i => TimeSpan.FromSeconds(3),
                        (result, timeSpan, retryCount, context) => { Logger.LogMessage("retrying" + requestUrl); }
                    );


                switch (requestMethod)
                {
                    case "GET":
                    {
                        var response = await retryPolicy.ExecuteAsync(() => _client.GetAsync(requestUrl));
                        var executed= checkValue(pollUntill, response.Content.ReadAsStringAsync().Result.ToString());
                        if(executed)
                        {
                            return response;
                        }

                        break;
                    }



                    case "POST":
                    {
                        var requestBody = new StringContent(requestConfig["body"].ToString(), Encoding.UTF8,
                            "application/json");

                        var response = await _client.PostAsync(requestUrl, requestBody);

                        return response;


                    }

                    default:
                        throw new Exception(
                            $"{requestMethod} not supported. Please add client call for this method");
                }

                return null;
            }



            private async Task AddRequestHeaders(JToken headers)
            {
                
                
                var auth = new Authorization(AuthorizationType.EnterpriseApi);
                foreach (var header in ((JObject) headers).Properties())
                {
                    var _token=await auth.GetAuthorized();
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.AccessToken);
                }
            }


            public string ReadBaseUrl(JToken requestConfig)
            {
                var url = requestConfig["baseUrl"].ToString();
                  var env= "QA";


                  switch (env)
                  {
                      case (TestEnvironment.QA):
                          switch (url)
                          {
                              case ("RuleEngineOrchestrator"):
                                  return ReferralOrchestratorEnumsQA.RuleEngineOrchestrator;
                              default:
                                  return null;

                          }

                      case (TestEnvironment.UAT):

                          switch (url)
                          {
                              case ("RuleEngineBaseUrlUAT"):
                                  return ReferralOrchestratorEnumsQA.RuleEngineOrchestrator;
                              default:
                                  return null;
                          }
                          
                          default:
                              return null;

                  }
            }

            public bool checkValue(JToken pollUntill, String response)
            {
                try
                {
                    var Response = JObject.Parse(response);
                    var path = pollUntill.SelectToken("path");
                    var value =pollUntill.SelectToken("value");
                    var selectedcontent = Response.SelectToken(path.ToString());
                    if (selectedcontent.Equals(value))
                    {
                        return true;

                    }

                    
                }
                catch (Exception e)
                {
                   Console.WriteLine(e.Message + "Expected poll value not achieved");
                }
                return false;
            }

            public void Dispose()
            {
                _client.Dispose();
            }
            
        }
    }
    
