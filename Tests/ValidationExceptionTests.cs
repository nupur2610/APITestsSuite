    using System.Threading.Tasks;
    using ReferralOrchAPITest.SetUp;
    using Xunit;
    using Xunit.Abstractions;

    namespace ReferralOrchAPITest.Tests
    {
        public class ValidationExceptionTests:BaseOrchestratorTests
        {
            public ValidationExceptionTests(GlobalSetUpFixture fixture, ITestOutputHelper output) : base(fixture, output)
            {
            }

            [Fact]
            public async Task ValidationfailureTests() => 
                await ReferralOrchestrationTests("ValidationException.json");
        }
        }
