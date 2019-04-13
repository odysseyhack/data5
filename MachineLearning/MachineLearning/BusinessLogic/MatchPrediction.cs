using Microsoft.ML.Data;

namespace MachineLearning.BusinessLogic
{
    public class MatchPrediction
    {
        [ColumnName("PredictedLabel")]
        public uint PredictedClusterId;

        [ColumnName("Score")]
        public float[] Distances;
    }
}
