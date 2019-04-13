using MachineLearning.BusinessLogic;
using MachineLearning.Controllers;
using MachineLearning.V1.Controllers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Moq;

using Xunit;

namespace MachineLearning.Tests
{
    public class PatternControllerTests
    {
        public Mock<IMatcher> matcher;
        public Mock<IConfiguration> config;
        public Mock<ILogger<PatternController>> logger;

        public PatternControllerTests()
        {
            var matchPrediction = new MatchPrediction { PredictedClusterId = 14, Distances = new[] { (float)1.1, (float)0.5, (float)0.9 } };
            this.matcher = new Mock<IMatcher>();
            this.matcher.Setup(m => m.Match(It.IsAny<ModelData>(), out matchPrediction)).Returns(true);

            this.config = new Mock<IConfiguration>();
            this.logger = new Mock<ILogger<PatternController>>();
        }

        [Fact]
        public void TestPatternControllerCreate()
        {
            // Setup
            var controller = new PatternController(this.matcher.Object, this.config.Object, this.logger.Object);

            // Check
            Assert.NotNull(controller);
        }

        [Fact]
        public void TestPatternControllerPost()
        {
            // Setup
            var controller = new PatternController(this.matcher.Object, this.config.Object, this.logger.Object);
            var data = new MatchData
            {
                Features = new[] { (float)0.3, (float)0.4, (float)0.5, (float)0.6, (float)0.7, (float)0.8 }
            };

            // Test
            var response = controller.Post(data);

            // Check
            var matchResponse = ((OkObjectResult)response).Value as MatchResponse;
            Assert.Equal<uint>(14, matchResponse.Id);
        }

        [Fact]
        public void TestPatternControllerPut()
        {
            // Setup
            var controller = new PatternController(this.matcher.Object, this.config.Object, this.logger.Object);
            var dataset = new LearnDataSet
            {
                DataPath = "Test/Test",
                RecordCount = 6,
                HasHeader = true,
                InputColumns = new[] { "Col0", "Col1", "Col2", "Col3" }
            };

            // Test
            var response = controller.Put(dataset);

            // Check
            Assert.Equal(200, ((OkResult)response).StatusCode);
        }
    }
}
