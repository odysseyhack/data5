using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace DatasetDownloader
{
    public class ProviderConnector : IProviderConnector
    {
        private List<string> AwaitedDownloads { get; set; }

        private List<string> DownloadedFiles { get; set; }

        private const string downloadpath = @"C:\Temp\DataDownload";

        private bool isDownloading { get; set; }

        public void GetDatasetDataFile(string url, string type)
        {
            if (DownloadedFiles == null)
            {
                DownloadedFiles = new List<string>();
            }

            if (AwaitedDownloads == null)
            {
                AwaitedDownloads = new List<string>();
            }

            AwaitedDownloads.Add(url + ";" + type);
            if(!this.isDownloading)
            {
                this.DownloadFiles();
            }

            this.isDownloading = true;
        }

        private bool DownloadFiles()
        {
            if(this.AwaitedDownloads.Count == 0)
            {
                this.isDownloading = false;
                return true;
            }

            var url = this.AwaitedDownloads[0].Split(';')[0];
            var type = this.AwaitedDownloads[0].Split(';')[1];
            var filename = Path.Combine(downloadpath, Guid.NewGuid().ToString() + "." + type);

            this.AwaitedDownloads.RemoveAt(0);

            using (WebClient wc = new WebClient())
            {
                DownloadedFiles.Add(filename + ";" + type);
                // wc.DownloadProgressChanged += FileDownloaded;
                wc.DownloadFileCompleted += FileDownloaded;
                wc.DownloadFileAsync(new Uri(url), filename);
            }

            return true;
        }

        private void FileDownloaded(object sender, AsyncCompletedEventArgs e)
        {
            var filename = this.DownloadedFiles[0].Split(';')[0];
            var type = this.DownloadedFiles[0].Split(';')[1];
            this.DownloadedFiles.RemoveAt(0);

            // start next download
            //this.DownloadFiles();
            this.isDownloading = false;


            // read file
            using (var streamreader = new StreamReader(filename))
            {
                var data = streamreader.ReadToEnd();

                switch(type)
                {
                    case "csv":
                        this.ExtractCsvData(data);
                        break;
                }

                
                streamreader.Close();
            }
        }

        private void ExtractCsvData(string data)
        {
            var items = data.Replace("\r", string.Empty).Split("\n");
            if(items.Length > 1)
            {
                var delimeter = this.DetermineDelimiter(items[0], items[1]);
                foreach(var line in items)
                {
                    var splittedString = line.Split(delimeter);
                }
                
            }
        }

        public string DetermineDelimiter(string line1, string line2)
        {
            var count1 = line1.Split(',').Length;
            var count2 = line2.Split(',').Length;

            if(count1 != count2 || (count1 == 0 && count2 == 0))
            {
                count1 = line1.Split(';').Length;
                count2 = line2.Split(';').Length;
                return ";";
            }

            if (count1 != count2 || (count1 == 0 && count2 == 0))
            {
                count1 = line1.Split("\t").Length;
                count2 = line2.Split("\t").Length;
                return "\t";
            }

            return ",";
        }
    }
}
