
namespace MachineLearning.V1.Controllers.Model
{
    public class LearnDataSet
    {
        public string DataPath { get; set; }

        public int RecordCount { get; set; }

        public bool HasHeader { get; set; }

        public string[] InputColumns { get; set; }
    }
}
