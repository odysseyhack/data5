using System.Collections.Generic;
using System.Linq;
using MachineLearning.BusinessLogic;
using MachineLearning.V1.Controllers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MachineLearning.Controllers
{
    [ApiVersion("1")]
    [Produces("application/json")]
    [Route("v{version:apiVersion}/[controller]")]
    public class PatternController : ControllerBase
    {
        #region Fields

        private readonly IConfiguration configuration;
        private readonly ILogger<PatternController> logger;
        private readonly IMatcher matcher;

        #endregion Fields

        #region Constructors and Destructors

        public PatternController(IMatcher matcher, IConfiguration configuration, ILogger<PatternController> logger)
        {
            this.matcher = matcher;
            this.configuration = configuration;
            this.logger = logger;
        }

        #endregion Constructors and Destructors

        #region Methods

        [HttpPost]
        public IActionResult Post([FromBody] MatchData matchData)
        {
            this.logger.LogInformation("Features {0}", matchData.Features);

            var modelData = new ModelData { Columns = matchData.Features.ToArray<float>() };

            this.matcher.Match(modelData, out List<int> prediction);

            return Ok(new MatchResponse { Matched = true, Id = prediction });
        }

        [HttpPut]
        public IActionResult Put([FromBody] LearnDataSet dataSet)
        {
            this.logger.LogInformation("Learn {0}", dataSet.DataPath);

            this.matcher.Learn(dataSet.DataPath, dataSet.RecordCount, dataSet.HasHeader, dataSet.InputColumns);

            return Ok();
        }

        #endregion Methods
    }
}
