using DatasetDownloader.BusinessLogic;
using DatasetDownloader.BusinessLogic.Filetypes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;

namespace DatasetDownloader
{
    public class ProviderConnector : IProviderConnector
    {
        private const string downloadpath = @"C:\Temp\DataDownload";

        private IDataExtractions extractions { get; set; }
        private List<string> AwaitedDownloads { get; set; }
        private List<string> DownloadedFiles { get; set; }
        private bool isDownloading { get; set; }

        public ProviderConnector(IDataExtractions dataExtractions)
        {
            this.extractions = dataExtractions;
        }

        public void GetDatasetDataFile(string url, string type)
        {
            if (DownloadedFiles == null)
                DownloadedFiles = new List<string>();

            if(AwaitedDownloads == null)
                AwaitedDownloads = new List<string>();

            AwaitedDownloads.Add(url + ";" + type);
            if (!this.isDownloading)
                this.DownloadFiles();

            this.isDownloading = true;
        }

        public bool DownloadFiles()
        {
            if(this.AwaitedDownloads.Count == 0)
            {
                this.isDownloading = false;
                return true;
            }

            var filename = Path.Combine(downloadpath, Guid.NewGuid().ToString() + "." + this.AwaitedDownloads[0].Split(';')[1]);
            this.ExecuteDownload(filename);
            this.AwaitedDownloads.RemoveAt(0);

            return true;
        }

        public void ExecuteDownload(string filename)
        {
            using (WebClient wc = new WebClient())
            {
                DownloadedFiles.Add(filename + ";" + this.AwaitedDownloads[0].Split(';')[1]);
                wc.DownloadFileCompleted += FileDownloaded;
                wc.DownloadFileAsync(new Uri(this.AwaitedDownloads[0].Split(';')[0]), filename);
            }
        }

        public void FileDownloaded(object sender, AsyncCompletedEventArgs e)
        {
            this.isDownloading = false;
            var type = this.DownloadedFiles[0].Split(';')[1];
            if (type != "zip")
            {
                var filename = this.DownloadedFiles[0].Split(';')[0];
                using (var streamreader = new StreamReader(filename))
                {
                    this.DownloadedFiles.RemoveAt(0);
                    var data = streamreader.ReadToEnd();
                    switch (type)
                    {
                        case "csv":
                            (new CommaDelimited(extractions)).ExtractCsvData(data, filename);
                            break;
                        case "json":
                            (new JsonExtractor(extractions)).GetJsonExtraction(data, filename);
                            break;
                    }
                }
            }

            this.DownloadFiles();
        }
    }
}
