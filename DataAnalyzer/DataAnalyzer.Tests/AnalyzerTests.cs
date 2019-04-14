using DataAnalyzer.BusinessLogic;
using DataAnalyzer.DataAccess;
using DataAnalyzer.ServiceAgents;
using Microsoft.Extensions.Logging;

using Moq;

using Xunit;

namespace DataAnalyzer.Tests
{
    public class AnalyzerTests
    {
        public Mock<ILogger<Analyzer>> logger;

        public AnalyzerTests()
        {
            this.logger = new Mock<ILogger<Analyzer>>();
        }

        [Fact]
        public void TestMachineLearningAgentCreate()
        {
            // Setup
            var metaData = new Mock<IMetaDataAccess>();
            var machineLearning = new Mock<IMachineLearningAgent>();
            var analyzer = new Analyzer(metaData.Object, machineLearning.Object);

            // Test

            // Check
            Assert.NotNull(analyzer);
        }

        [Fact]
        public void TestMachineLearningAgentFindOverlapse()
        {
            // Setup
            var metaData = new Mock<IMetaDataAccess>();
            var machineLearning = new Mock<IMachineLearningAgent>();
            var analyzer = new Analyzer(metaData.Object, machineLearning.Object);

            // Test
            analyzer.FindOverlaps();

            // Check
            Assert.NotNull(analyzer);
        }
    }
}
