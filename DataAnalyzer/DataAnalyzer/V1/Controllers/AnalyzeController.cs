using System.Collections.Generic;
using DataAnalyzer.BusinessLogic;
using DataAnalyzer.DataEntities;
using DataAnalyzer.V1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DataAnalyzer.V1.Controllers
{
    [ApiVersion("1")]
    [Produces("application/json")]
    [Route("v{version:apiVersion}/[controller]")]
    public class AnalyzeController : ControllerBase
    {
        #region Fields

        private readonly IAnalyzer analyzer;
        private readonly IOptions<Settings> options;
        private readonly ILogger<AnalyzeController> logger;

        #endregion Fields

        #region Constructors and Destructors

        public AnalyzeController(IAnalyzer analyzer, IOptions<Settings> options)
        {
            this.analyzer = analyzer;
            this.options = options;
        }

        #endregion Constructors and Destructors

        #region Methods

        [HttpPost]
        public IActionResult Post([FromBody] DataSet dataSet)
        {
            this.analyzer.FindOverlaps();

            return Ok();
        }
        
        #endregion Methods
    }
}
