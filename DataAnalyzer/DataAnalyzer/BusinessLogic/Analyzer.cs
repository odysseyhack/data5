using DataAnalyzer.DataAccess;
using DataAnalyzer.DataEntities;
using DataAnalyzer.ServiceAgents;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DataAnalyzer.BusinessLogic
{
    public class Analyzer : IAnalyzer
    {
        #region Fields

        private IMetaDataAccess dataAccess;

        private IMachineLearningAgent learningAgent;

        private IOptions<Settings> options;

        private ILogger<Analyzer> logger;

        #endregion Fields

        #region Constructor

        public Analyzer(IMetaDataAccess dataAccess, IMachineLearningAgent learningAgent)
        {
            this.dataAccess = dataAccess;
            this.learningAgent = learningAgent;
        }

        #endregion Constructor

        #region Public Methods

        public void FindOverlaps()
        {
            var response = this.dataAccess.GetByFieldType("N4A2");

            var dataSet = new LearnDataSet
            {
                DataPath = "D:/Data/TestSet.csv", RecordCount = 5, HasHeader = true, InputColumns = new[] { "Columns" }
            };
            this.learningAgent.InitializeMachineLearning("http://localhost:57476/v1/pattern", dataSet);

            var matchData = new MatchData
            {
                Features = new[] { (float)0.1, (float)0.2, (float)0.3, (float)0.4, (float)0.5, (float)0.6, (float)0.7 }
            };
            this.learningAgent.FindMatch("http://localhost:57476/v1/pattern", matchData);
        }

        #endregion Public Methods
    }
}
