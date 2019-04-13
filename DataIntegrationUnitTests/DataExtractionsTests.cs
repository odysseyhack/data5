using DatasetDownloader.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataIntegrationUnitTests
{
    [TestClass]
    public class DataExtractionsTests
    {
        [TestMethod]
        public void DataExtractionTest1()
        {
            DataExtractions de = new DataExtractions();
            string[] data = null;
            using (var reader = new StreamReader(@".\Testdata\CsvDelimited.csv"))
            {
                data = reader.ReadToEnd().Replace("\r", string.Empty).Split('\n');
            }

            var test = de.ExecuteDataInformationExtraction(data, ";", "banana.txt");
            Assert.AreEqual(test.DataFieldAnalysis.Count, 1);
        }

        [TestMethod]
        public void DataExtractionTest2()
        {
            DataExtractions de = new DataExtractions();
            string[] data = null;
            using (var reader = new StreamReader(@".\Testdata\CsvDelimited.csv"))
            {
                data = reader.ReadToEnd().Replace("\r", string.Empty).Split('\n');
            }

            var test = de.ExecuteDataInformationExtraction(data, ";", "banana.txt");
            Assert.AreEqual(test.DataFieldAnalysis[0].AverageFieldLength, 157);
        }

        [TestMethod]
        public void DataExtractionTest3()
        {
            DataExtractions de = new DataExtractions();
            string[] data = null;
            using (var reader = new StreamReader(@".\Testdata\CsvDelimited.csv"))
            {
                data = reader.ReadToEnd().Replace("\r", string.Empty).Split('\n');
            }

            var test = de.ExecuteDataInformationExtraction(data, ";", "banana.txt");
            Assert.AreEqual(test.DataFieldAnalysis[0].ConsistentDataType, "N37;A5;N1;A1;N3;A14;N2;A55;N1;A16");
        }

        [TestMethod]
        public void DataExtractionTest4()
        {
            DataExtractions de = new DataExtractions();
            string[] data = null;
            using (var reader = new StreamReader(@".\Testdata\CsvDelimited.csv"))
            {
                data = reader.ReadToEnd().Replace("\r", string.Empty).Split('\n');
            }

            var test = de.ExecuteDataInformationExtraction(data, ";", "banana.txt");
            Assert.AreEqual(test.DataFieldAnalysis[0].DamerauValue, 13.4);
        }

        [TestMethod]
        public void DataExtractionTest5()
        {
            DataExtractions de = new DataExtractions();
            string[] data = null;
            using (var reader = new StreamReader(@".\Testdata\CsvDelimited.csv"))
            {
                data = reader.ReadToEnd().Replace("\r", string.Empty).Split('\n');
            }

            var test = de.ExecuteDataInformationExtraction(data, ";", "banana.txt");
            Assert.AreEqual(test.DataFieldAnalysis[0].FieldIsConsistent, true);
        }

        [TestMethod]
        public void DataExtractionTest6()
        {
            DataExtractions de = new DataExtractions();
            string[] data = null;
            using (var reader = new StreamReader(@".\Testdata\CsvDelimited.csv"))
            {
                data = reader.ReadToEnd().Replace("\r", string.Empty).Split('\n');
            }

            var test = de.ExecuteDataInformationExtraction(data, ";", "banana.txt");
            Assert.AreEqual(test.DataFieldAnalysis[0].MaximumFieldLength, 157);
        }

        [TestMethod]
        public void DataExtractionTest7()
        {
            DataExtractions de = new DataExtractions();
            string[] data = null;
            using (var reader = new StreamReader(@".\Testdata\CsvDelimited.csv"))
            {
                data = reader.ReadToEnd().Replace("\r", string.Empty).Split('\n');
            }

            var test = de.ExecuteDataInformationExtraction(data, ";", "banana.txt");
            Assert.AreEqual(test.DataFieldAnalysis[0].MinimumFieldLength, 157);
        }
    }
}
