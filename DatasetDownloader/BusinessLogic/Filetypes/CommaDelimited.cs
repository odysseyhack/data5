using System;
using System.Collections.Generic;
using System.Text;

namespace DatasetDownloader.BusinessLogic.Filetypes
{
    public class CommaDelimited
    {
        private IDataExtractions extractions { get; set; }

        public CommaDelimited(IDataExtractions extractions)
        {
            this.extractions = extractions;
        }

        public DataContracts.DataFieldMain ExtractCsvData(string data, string filename)
        {
            var items = data.Replace("\r", string.Empty).Split("\n");
            if (items.Length > 1)
            {
                var delimiter = this.DetermineDelimiter(items[0], items[1]);
                return this.extractions.ExecuteDataInformationExtraction(items, delimiter, filename);
            }

            return null;
        }

        public string DetermineDelimiter(string line1, string line2)
        {
            var delimiters = new[] { ';', ',', '\t' };
            foreach (var delimiter in delimiters)
            {
                var count1 = line1.Split(delimiter).Length;
                var count2 = line2.Split(delimiter).Length;
                if (count1 == count2 && (count1 > 1 && count2 > 1))
                {
                    return delimiter.ToString();
                }
            }

            throw new Exception("No delimiter data found, please check this set manually.");
        }
    }
}
