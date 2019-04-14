using System;
using System.Collections.Generic;
using System.Text;

namespace DatasetDownloader
{
    public interface IProviderConnector
    {
        void GetDatasetDataFile(string url, string type, string databaseUrl);
    }
}
