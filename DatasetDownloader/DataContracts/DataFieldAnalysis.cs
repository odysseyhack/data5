using System;
using System.Collections.Generic;
using System.Text;

namespace DatasetDownloader.DataContracts
{
    public class DataFieldMain
    {
        public string CleansetFilename { get; set; }
        public List<DataFieldAnalysis> DataFieldAnalysis { get; set; }

        public string OriginalsetFilename { get; set; }
    }
    public class DataFieldAnalysis
    {
        public enum FieldTypes
        {
            Dec,
            Int,
            Alfanumeric,
            Long,
            Short,
            Bool
        }

        public string FieldName { get; set; }
        public int AverageFieldLength { get; set; }
        public int MaximumFieldLength { get; set; }
        public int MinimumFieldLength { get; set; }
        public List<FieldTypes> FieldType { get; set; }
        public List<string> FieldConsistenceList { get; set; }
        public bool FieldIsConsistent { get; set; }
        public string ConsistentDataType { get; set; }
        public float DamerauValue { get; set; }
    }
}
