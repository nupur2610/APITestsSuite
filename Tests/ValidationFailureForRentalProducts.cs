    using System.Threading.Tasks;
    using ReferralOrchAPITest.SetUp;
    using Xunit;
    using Xunit.Abstractions;

    namespace ReferralOrchAPITest.Tests
    {
        public class ValidationFailureForRentalProducts:BaseOrchestratorTests
        {
            public ValidationFailureForRentalProducts(GlobalSetUpFixture fixture, ITestOutputHelper output) : base(fixture, output)
            {
            }

            [Fact]
            public async Task ValidationFailureforRentalTests() => 
                await ReferralOrchestrationTests("ValidationFailureForRentalProducts.json");
        }
            
        }
