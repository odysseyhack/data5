using System.Collections.Generic;
using DataAnalyzer.DataEntities;
using DotNetCore.Entities;

namespace DataAnalyzer.DataAccess
{
    public interface IMetaDataAccess
    {
        IList<MetaData> GetByFieldType(string type);
        IList<MetaData> GetByOds(int ods);
    }
}