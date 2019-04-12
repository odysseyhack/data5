using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace DataIntergration.Controllers
{
    public class DatasetReadController : Controller
    {
        DatasetDownloader.IProviderConnector provider { get; set; }
        public DatasetReadController(DatasetDownloader.IProviderConnector provider)
        {
            this.provider = provider;
        }

        public async void ReadDataSet()
        {
            using (var streamreader = new StreamReader(@".\Urls\Urls.txt"))
            {
                var items = streamreader.ReadToEnd().Replace("\r", string.Empty).Split("\n");
                foreach(var line in items)
                {
                    var itemset = line.Split(';');
                    if (itemset.Length > 1)
                    {
                        provider.GetDatasetDataFile(itemset[3], itemset[0].Split(':')[1]);
                    }
                }
                streamreader.Close();
            }
         }
    }
}
