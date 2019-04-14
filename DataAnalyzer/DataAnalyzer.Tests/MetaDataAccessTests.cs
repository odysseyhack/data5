using DataAnalyzer.DataAccess;
using Microsoft.Extensions.Logging;

using Moq;

using Xunit;

namespace DataAnalyzer.Tests
{
    public class MetaDataAccessTests
    {
        public Mock<ILogger<MetaDataAccess>> logger;

        public MetaDataAccessTests()
        {
            this.logger = new Mock<ILogger<MetaDataAccess>>();
        }

        [Fact]
        public void TestMachineLearningAgentCreate()
        {
            // Setup
            var options = new OptionsMock();
            var agent = new MetaDataAccess(options, this.logger.Object);

            // Test

            // Check
            Assert.NotNull(agent);
        }

        [Fact]
        public void TestMachineLearningAgentGetByOds()
        {
            // Setup
            var options = new OptionsMock();
            var agent = new MetaDataAccess(options, this.logger.Object);

            // Test
            var response = agent.GetByOds(0);

            // Check
            Assert.NotNull(response);
            Assert.Equal(1, response.Count);
            Assert.Equal(1, response[0].Id);
        }

        [Fact]
        public void TestMachineLearningAgentGetByFieldType()
        {
            // Setup
            var options = new OptionsMock();
            var agent = new MetaDataAccess(options, this.logger.Object);

            // Test
            var response = agent.GetByFieldType("N4A2");

            // Check
            Assert.NotNull(response);
            Assert.Equal(1, response.Count);
            Assert.Equal(1, response[0].Id);
        }
    }
}
