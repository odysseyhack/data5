using System.Collections.Generic;
using DataAnalyzer.DataEntities;

namespace DataAnalyzer.DataAccess
{
    public interface IMetaDataAccess
    {
        IList<MetaData> GetByFieldType(string type);
        IList<MetaData> GetByOds(int ods);
    }
}