    using System.Threading.Tasks;
    using ReferralOrchAPITest.SetUp;
    using Xunit;
    using Xunit.Abstractions;

    namespace ReferralOrchAPITest.Tests
    {
        public class LomnAuthorizationTests : BaseOrchestratorTests
        {
            public LomnAuthorizationTests(GlobalSetUpFixture fixture, ITestOutputHelper output) : base(fixture, output)
            {
            }

            [Fact]
            public async Task CheckLOmnOrchestration() => 
                await ReferralOrchestrationTests("LomnAuthorization.json");
        }
    }