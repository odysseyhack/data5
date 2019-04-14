using DataAnalyzer.DataEntities;

namespace DataAnalyzer.ServiceAgents
{
    public interface IMachineLearningAgent
    {
        MatchResponse FindMatch(string uri, MatchData matchData);
        void InitializeMachineLearning(string uri, LearnDataSet learnDataSet);
    }
}