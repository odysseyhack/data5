using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using Dapper;
using DataAnalyzer.DataEntities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DataAnalyzer.DataAccess
{
    public class MetaDataAccess : IMetaDataAccess
    {
        #region Fields

        private IOptions<Settings> options;

        private ILogger<MetaDataAccess> logger;

        private SqlConnection connection;

        #endregion Fields

        #region Constructor

        public MetaDataAccess(IOptions<Settings> options, ILogger<MetaDataAccess> logger)
        {
            this.options = options;
            this.logger = logger;
        }

        #endregion Constructor

        #region Public Methods

        public IList<MetaData> GetByOds(int ods)
        {
            var connection = this.GetConnection();

            return connection.Query<MetaData>("SELECT * FROM odsmetadata2 WHERE ods_id = @ods", new { ods = ods }).ToList();
        }

        public IList<MetaData> GetByFieldType(string type)
        {
            var connection = this.GetConnection();

            return connection.Query<MetaData>("SELECT * FROM odsmetadata2 WHERE FieldType = @type", new { type = type }).ToList();
        }

        #endregion Public Methods

        #region Private Methods

        private SqlConnection GetConnection()
        {
            if (connection == null)
            {
                this.connection = new SqlConnection(options.Value.ConnectionString);
            }

            return this.connection;
        }

        #endregion Private Methods
    }
}
