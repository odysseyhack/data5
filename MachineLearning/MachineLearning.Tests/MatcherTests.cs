using System;
using System.Collections.Generic;
using System.Text;

using MachineLearning.BusinessLogic;
using Microsoft.Extensions.Logging;

using Moq;

using Xunit;

namespace MachineLearning.Tests
{
    public class MatcherTests
    {
        public Mock<ILogger<Matcher>> logger;

        public MatcherTests()
        {
            this.logger = new Mock<ILogger<Matcher>>();
        }

        [Fact]
        public void TestMatcherCreate()
        {
            // Setup
            var matcher = new Matcher(this.logger.Object);

            // Check
            Assert.NotNull(matcher);
        }

        [Fact]
        public void TestmatcherLearn()
        {
            // Setup
            var matcher = new Matcher(this.logger.Object);

            // Test
            var response = matcher.Learn(@".\Data\TestSet.csv", true, new[] { "Columns" });

            // Check
            Assert.True(response);
        }

        [Fact]
        public void TestmatcherMatch()
        {
            // Setup
            var matcher = new Matcher(this.logger.Object);
            matcher.Learn(@".\Data\TestSet.csv", true, new[] { "Columns" });

            // Test
            var response = matcher.Match(new ModelData { Columns = new[] { (float)0.1, (float)0.2, (float)0.3, (float)0.4, (float)0.5, (float)0.6, (float)0.7 } }, out MatchPrediction matchPrediction);

            // Check
            Assert.True(response);
        }
    }
}
