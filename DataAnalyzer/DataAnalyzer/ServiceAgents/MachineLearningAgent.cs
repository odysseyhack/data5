using System.Net.Http;
using System.Text;

using DataAnalyzer.DataEntities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DataAnalyzer.ServiceAgents
{
    public class MachineLearningAgent : IMachineLearningAgent
    {
        #region Fields

        private ILogger<MachineLearningAgent> logger;

        #endregion Fields

        #region Constructor

        public MachineLearningAgent(ILogger<MachineLearningAgent> logger)
        {
            this.logger = logger;
        }

        #endregion Constructor

        #region Public Methods

        public void InitializeMachineLearning(string uri, LearnDataSet learnDataSet)
        {
            using (var client = new HttpClient())
            {
                var response = client.PutAsync(uri, new StringContent(JsonConvert.SerializeObject(learnDataSet), Encoding.UTF8, "application/json")).Result;
                this.logger.LogInformation("Setup Machine Learning");
            }
        }

        public MatchResponse FindMatch(string uri, MatchData matchData)
        {
            this.logger.LogInformation("Find match");

            var message = string.Empty;

            using (var client = new HttpClient())
            {
                var reply = client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(matchData), Encoding.UTF8, "application/json")).Result;
                message = reply.Content.ReadAsStringAsync().Result;
            }

            var response = JsonConvert.DeserializeObject<MatchResponse>(message);

            this.logger.LogInformation("Found match with row {0}", response.Id);

            return response;
        }

        #endregion Public Methods
    }
}
