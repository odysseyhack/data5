using Microsoft.Data.DataView;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using System.Collections.Generic;
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

        private Dictionary<int, int> translateMap;

        #endregion Fields

        #region Constructor

        public Matcher(ILogger<Matcher> logger)
        {
            this.logger = logger;

            this.mlContext = new MLContext();
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

            var outDataView = model.Transform(dataView);

            this.translateMap = new Dictionary<int, int>();

            var schema = outDataView.Schema;
            var cur = outDataView.GetRowCursor(schema);
            while (cur.MoveNext())
            {
                uint value = 0;
                cur.GetGetter<uint>(2).Invoke(ref value);
                this.translateMap.Add((int)cur.Position, (int)value);
            };

            return true;
        }

        public bool Match(ModelData matchData, out List<int> indexes)
        {
            var prediction = this.predictionEngine.Predict(matchData);

            var index = (int)prediction.PredictedClusterId;
            indexes = this.translateMap.Where(t => t.Value == index).Select(t => t.Key).ToList();

            this.logger.LogInformation($"Cluster: {prediction.PredictedClusterId}");
            this.logger.LogInformation($"indexes: {string.Join(" ", indexes)}");
            this.logger.LogInformation($"Distance: { prediction.Distances[prediction.PredictedClusterId - 1] }");

            return true;
        }

        #endregion Public Methods
    }
}
