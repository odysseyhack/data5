using Dapper;
using DatasetDownloader.DataContracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DatasetDownloader.DataAccess
{
    public interface IMetaDataAccess
    {
        int GetMaxOds(string databaseConnection);

        bool InsertDataFieldData(DataFieldMain Datafield, string databaseConnection);
    }
}
