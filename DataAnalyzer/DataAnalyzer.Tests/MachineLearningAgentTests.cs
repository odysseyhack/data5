using DataAnalyzer.DataEntities;
using DataAnalyzer.ServiceAgents;
using Microsoft.Extensions.Logging;

using Moq;

using Xunit;

namespace DataAnalyzer.Tests
{
    public class MachineLearningAgentTests
    {
        public Mock<ILogger<MachineLearningAgent>> logger;

        public MachineLearningAgentTests()
        {
            this.logger = new Mock<ILogger<MachineLearningAgent>>();
        }

        [Fact]
        public void TestMachineLearningAgentCreate()
        {
            // Setup
            var agent = new MachineLearningAgent(this.logger.Object);

            // Test

            // Check
            Assert.NotNull(agent);
        }

        [Fact]
        public void TestMachineLearningAgentInitializeMachineLearning()
        {
            // Setup
            var agent = new MachineLearningAgent(this.logger.Object);

            // Test
            agent.InitializeMachineLearning(@"http://localhost:57476/v1/pattern", new LearnDataSet
            {
                DataPath = "D:/Data/TestSet.csv",
                RecordCount = 5,
                HasHeader = true,
                InputColumns = new[] { "Columns" }
            });

            // Check
            Assert.NotNull(agent);
        }

        [Fact]
        public void TestMachineLearningAgentFindMatch()
        {
            // Setup
            var agent = new MachineLearningAgent(this.logger.Object);
            agent.InitializeMachineLearning(@"http://localhost:57476/v1/pattern", new LearnDataSet
            {
                DataPath = "D:/Data/TestSet.csv", RecordCount = 5, HasHeader = true, InputColumns = new[] { "Columns" }
            });

            // Test
            var response = agent.FindMatch(@"http://localhost:57476/v1/pattern", new MatchData
            {
                Features = new[] { (float)0.1, (float)0.2, (float)0.3, (float)0.4, (float)0.5, (float)0.6, (float)0.7 }
            });

            // Check
            Assert.True(response.Matched);
            Assert.Equal(0, response.Id[0]);
        }
    }
}
