using Microsoft.ML.Data;

namespace MachineLearning.BusinessLogic
{
    public class ModelData
    {
        [LoadColumn(0,6)]
        [VectorType(7)]
        public float[] Columns;
    }
}
