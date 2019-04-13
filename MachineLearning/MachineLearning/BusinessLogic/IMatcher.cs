﻿
namespace MachineLearning.BusinessLogic
{
    public interface IMatcher
    {
        bool Learn(string dataPath, int count, bool hasHeader, string[] inputColumns);

        bool Match(ModelData matchData, out MatchPrediction prediction);
    }
}
