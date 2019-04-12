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
                        switch (itemset[0].Split(':')[1])
                        {
                            case "xls":
                                break;
                            case "xlsx":
                                break;
                            case "pdf":
                                break;
                            case "csv":
                                provider.GetDatasetDataFile(itemset[3], "csv");
                                break;
                            case "xml":
                                break;
                            case "json":
                                break;
                            case "rss":
                                break;
                            case "zip":
                                break;


                        }
                    }
                }

                streamreader.Close();
            }
         }

    }
}
