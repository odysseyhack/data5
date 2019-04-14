namespace DatasetDownloader.DataContracts
{
    public class OpenMainData
    {
        public int id { get; set; }
        public int filename { get; set; }
    }

    public class MetaData
    {
        public int Id { get; set; }

        public int Ods_Id { get; set; }

        public int FieldIndex { get; set; }

        public string FieldName { get; set; }

        public int AverageFieldLength { get; set; }

        public int MaximumFieldLength { get; set; }

        public int MinimumFieldLength { get; set; }

        public string FieldType { get; set; }

        public string FieldConsistenceList { get; set; }

        public string FieldIsConsistent { get; set; }

        public string ConsistentDataType { get; set; }

        public float DamerauValue { get; set; }
    }
}
