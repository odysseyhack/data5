using DatasetDownloader.DataContracts;

namespace DatasetDownloader.BusinessLogic
{
    public interface IDataExtractions
    {
        DataFieldMain ExecuteDataInformationExtraction(string[] items, string delimiter, string filename);
    }
}
