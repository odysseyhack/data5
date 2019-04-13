using Microsoft.Data.DataView;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using System.Linq;

namespace MachineLearning.BusinessLogic
{
    public class Matcher : IMatcher
    {
        #region Constants

        private const string featuresColumnName = "Features";

        #endregion Constants

        #region Fields

        private readonly MLContext mlContext;

        private readonly ILogger<Matcher> logger;

        private PredictionEngine<ModelData, MatchPrediction> predictionEngine;

        #endregion Fields

        #region Constructor

        public Matcher(ILogger<Matcher> logger)
        {
            this.mlContext = new MLContext();
            this.logger = logger;
        }

        #endregion Constructor

        #region Public Methods

        public bool Learn(string dataPath, int count, bool hasHeader, string[] inputColumns)
        {
            IDataView dataView = mlContext.Data.LoadFromTextFile<ModelData>(dataPath, ';', hasHeader);

            var pipeline = this.mlContext.Transforms
                .Concatenate(featuresColumnName, inputColumns)
                .Append(mlContext.Clustering.Trainers.KMeans(featuresColumnName, clustersCount: count));
                
            var model = pipeline.Fit(dataView);

            this.predictionEngine = model.CreatePredictionEngine<ModelData, MatchPrediction>(this.mlContext);

            return true;
        }

        public bool Match(ModelData matchData, out MatchPrediction prediction)
        {
            prediction = this.predictionEngine.Predict(matchData);

            this.logger.LogInformation($"Cluster: {prediction.PredictedClusterId}");
            // this.logger.LogInformation($"Distances: {string.Join(" ", prediction.Distances)}");
            this.logger.LogInformation($"Distance: { prediction.Distances[prediction.PredictedClusterId - 1] }");

            return true;
        }

        #endregion Public Methods
    }
}
