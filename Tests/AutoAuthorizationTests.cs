    using System.Threading.Tasks;
    using ReferralOrchAPITest.SetUp;
    using Xunit;
    using Xunit.Abstractions;

    namespace ReferralOrchAPITest.Tests
    {
        public class AutoAuthorizationTests : BaseOrchestratorTests
        {
            public AutoAuthorizationTests(GlobalSetUpFixture fixture, ITestOutputHelper output) : base(fixture, output)
            {
            }

            [Fact]
            public async Task CheckAutoOrchestration() => 
                await ReferralOrchestrationTests("AutoAuthorization.json");
        }
    }