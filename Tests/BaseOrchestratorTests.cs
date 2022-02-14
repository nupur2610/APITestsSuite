    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using FluentAssertions;
    using JsonDiffPatchDotNet;
    using Newtonsoft.Json.Linq;
    using ReferralOrchAPITest.Helper;
    using ReferralOrchAPITest.SetUp;
    using Xunit;
    using Xunit.Abstractions;

    namespace ReferralOrchAPITest.Tests
    {
        public class BaseOrchestratorTests : IClassFixture<GlobalSetUpFixture>
        {
            private readonly GlobalSetUpFixture _fixture;
            private readonly ITestOutputHelper _output;
            private readonly JsonDiffPatch _jsonDiffFinder;

            public BaseOrchestratorTests(GlobalSetUpFixture fixture, ITestOutputHelper output)
            {
                _fixture = fixture;
                _output = output;
                _jsonDiffFinder = new JsonDiffPatch();
            }


            protected async Task ReferralOrchestrationTests(string fileName)
            {
                 var (name, api)=  TestCases(fileName);
                _output.WriteLine($"*************** {name} ***************");
                var mappedValues = new Dictionary<string, string>();
                foreach (var apireq in api)
                { 
                    var expectedStatusCode = (HttpStatusCode) Enum.Parse(typeof(HttpStatusCode), apireq["response"]["status_code"].ToString());
                    var response = await _fixture.Client.Send(apireq["request"], mappedValues);
                    response.StatusCode.Should().Be(expectedStatusCode);
                    if (!apireq["name"].ToString().Equals("Send Authorization Request"))
                    {
                        var actualResponseBody = JToken.Parse(response.Content.ReadAsStringAsync().Result);
                        if (apireq["response"]["compareResponse"] != null)
                        {
                            var path = apireq["response"]["compareResponse"].ToString();
                            actualResponseBody = actualResponseBody.SelectToken(path);
                            var expectedResponseBody = apireq["response"]["body"];
                            _output.WriteLine($"*************** Actual Response body: {actualResponseBody}********************** ExpectedResponse: {expectedResponseBody} ***************");
                            _jsonDiffFinder.Diff(actualResponseBody, expectedResponseBody)?.ToString().Should()
                                .BeNullOrWhiteSpace();
                            
                        }
                        _output.WriteLine($"stage complted.......{apireq["name"].ToString()}");
                        if (apireq["response"]["saveValue"] != null)
                        {
                            var saveValue = actualResponseBody.SelectToken(apireq["response"]["saveValue"].ToString())
                                .ToString();
                            _output.WriteLine($"value saved {saveValue}");
                            mappedValues.Add(apireq["response"]["saveValue"].ToString(), saveValue);
                        }

                        if (apireq["checkEvent"] != null)
                        {
                            Tuple<string , string> tuple= new Tuple<string, string>(apireq["checkEvent"]["Type"].ToString(),apireq["checkEvent"]["ReferralId"].ToString());
                            var body = GlobalSetUpFixture.Events[tuple].ToString(); 
                            var expectedbody = apireq["checkEvent"]["data"]["lineItems"];
                           var actualbody= JToken.Parse(body).SelectToken("$.lineItems");
                           _output.WriteLine(
                               $"***************Actual Response body matches Expected: Actual Response: {actualbody} ExpectedResponse: {expectedbody} ***************");
                           
                           _jsonDiffFinder.Diff(actualbody, expectedbody)?.ToString().Should()
                               .BeNullOrWhiteSpace();
                            
                        }
                    }
                }
                _output.WriteLine($"message list count: {GlobalSetUpFixture.Events.Count}");
                
            }

            private static (string, JToken) TestCases(string fileName)
            {
                var fileContent = JObject.Parse(File.ReadAllText(Utils.ExpectationFileFullPath(fileName)));
                var name = fileContent.Properties().FirstOrDefault()?.Name;
                var api = fileContent.Properties().FirstOrDefault().Value["api"];
                return (name, api);
            }
        }
    }