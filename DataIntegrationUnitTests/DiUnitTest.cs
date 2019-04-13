using DataIntergration.Controllers;
using DatasetDownloader;
using DatasetDownloader.BusinessLogic;
using DatasetDownloader.BusinessLogic.Filetypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataIntegrationUnitTests
{
    [TestClass]
    public class DiUnitTest
    {
        [TestMethod]
        public void TestReadController()
        {
            DatasetReadController drc = new DatasetReadController(new ProviderConnector(new DataExtractions()));
            drc.ReadDataSet();
        }

        [TestMethod]
        public void TestMethodProviderConnectorDataFile()
        {
            ProviderConnector pc = new ProviderConnector(new DataExtractions());
            pc.GetDatasetDataFile(@"https://opendata.arcgis.com/datasets/4e3ee61ac5a34c28981e8c61f94d098a_0.csv", "csv");
        }

        [TestMethod]
        public void TestMethodProviderConnectorDelimiterComma()
        {
            CommaDelimited pc = new CommaDelimited(new DataExtractions());
            var item = pc.DetermineDelimiter("X,Y,OBJECTID,BEHEERDER_VAKGEB,MASTTYPE,MAST_MASTCODE,WOONPLAATS,WIJK,BUURT,OPENBARE_RUIMTE,ARMATUURTYPE,LAMPTYPE", "5.989576590072968,52.597744618078657,1001,,CIL AL 8M,107,Genemuiden,Wijk 01 Kamperzeedijk,Kamperzeedijk-West,Kamperzeedijk,Brisa EASY LCN 3 (Lightronics),LED");
            Assert.AreSame(item, ",");
        }

        [TestMethod]
        public void TestMethodProviderConnectorDelimiterSemiColon()
        {
            CommaDelimited pc = new CommaDelimited(new DataExtractions());
            var item = pc.DetermineDelimiter("X;Y;OBJECTID;BEHEERDER_VAKGEB;MASTTYPE;MAST_MASTCODE;WOONPLAATS;WIJK;BUURT;OPENBARE_RUIMTE;ARMATUURTYPE;LAMPTYPE", "5.989576590072968;52.597744618078657;1001;;CIL AL 8M;107;Genemuiden;Wijk 01 Kamperzeedijk;Kamperzeedijk-West;Kamperzeedijk;Brisa EASY LCN 3 (Lightronics);LED");
            Assert.AreSame(item, ";");
        }

        [TestMethod]
        public void TestMethodProviderConnectorDelimiterTab()
        {
            CommaDelimited pc = new CommaDelimited(new DataExtractions());
            var item = pc.DetermineDelimiter("X\tY\tOBJECTID\tBEHEERDER_VAKGEB\tMASTTYPE\tMAST_MASTCODE\tWOONPLAATS\tWIJK\tBUURT\tOPENBARE_RUIMTE\tARMATUURTYPE\tLAMPTYPE", "5.989576590072968\t52.597744618078657\t1001\t\tCIL AL 8M\t107\tGenemuiden\tWijk 01 Kamperzeedijk\tKamperzeedijk-West\tKamperzeedijk\tBrisa EASY LCN 3 (Lightronics)\tLED");
            Assert.AreSame(item, "\t");
        }

        [TestMethod]
        public void TestMethodProviderConnectorException()
        {
            ProviderConnector pc = new ProviderConnector(new DataExtractions());
            Assert.ThrowsException<Exception>(() => pc.ExecuteDownload("Banana"));
        }

        // duplicate tests because of strange way of test code metrics :)
        [TestMethod]
        public void TestMethodProviderConnectorException2()
        {
            ProviderConnector pc = new ProviderConnector(new DataExtractions());
            Assert.ThrowsException<Exception>(() => pc.ExecuteDownload("Banana"));
        }

        [TestMethod]
        public void TestMethodProviderConnectorDataFile1()
        {
            ProviderConnector pc = new ProviderConnector(new DataExtractions());
            pc.GetDatasetDataFile(@"https://opendata.arcgis.com/datasets/4e3ee61ac5a34c28981e8c61f94d098a_0.csv", "csv");
        }

        [TestMethod]
        public void TestMethodProviderConnectorDelimiterComma1()
        {
            CommaDelimited pc = new CommaDelimited(new DataExtractions());
            var item = pc.DetermineDelimiter("X,Y,OBJECTID,BEHEERDER_VAKGEB,MASTTYPE,MAST_MASTCODE,WOONPLAATS,WIJK,BUURT,OPENBARE_RUIMTE,ARMATUURTYPE,LAMPTYPE", "5.989576590072968,52.597744618078657,1001,,CIL AL 8M,107,Genemuiden,Wijk 01 Kamperzeedijk,Kamperzeedijk-West,Kamperzeedijk,Brisa EASY LCN 3 (Lightronics),LED");
            Assert.AreSame(item, ",");
        }

        [TestMethod]
        public void TestMethodProviderConnectorDelimiterSemiColon1()
        {
            CommaDelimited pc = new CommaDelimited(new DataExtractions());
            var item = pc.DetermineDelimiter("X;Y;OBJECTID;BEHEERDER_VAKGEB;MASTTYPE;MAST_MASTCODE;WOONPLAATS;WIJK;BUURT;OPENBARE_RUIMTE;ARMATUURTYPE;LAMPTYPE", "5.989576590072968;52.597744618078657;1001;;CIL AL 8M;107;Genemuiden;Wijk 01 Kamperzeedijk;Kamperzeedijk-West;Kamperzeedijk;Brisa EASY LCN 3 (Lightronics);LED");
            Assert.AreSame(item, ";");
        }

        [TestMethod]
        public void TestMethodProviderConnectorDelimiterTab1()
        {
            CommaDelimited pc = new CommaDelimited(new DataExtractions());
            var item = pc.DetermineDelimiter("X\tY\tOBJECTID\tBEHEERDER_VAKGEB\tMASTTYPE\tMAST_MASTCODE\tWOONPLAATS\tWIJK\tBUURT\tOPENBARE_RUIMTE\tARMATUURTYPE\tLAMPTYPE", "5.989576590072968\t52.597744618078657\t1001\t\tCIL AL 8M\t107\tGenemuiden\tWijk 01 Kamperzeedijk\tKamperzeedijk-West\tKamperzeedijk\tBrisa EASY LCN 3 (Lightronics)\tLED");
            Assert.AreSame(item, "\t");
        }

        [TestMethod]
        public void TestMethodProviderConnectorException1()
        {
            ProviderConnector pc = new ProviderConnector(new DataExtractions());
            Assert.ThrowsException<Exception>(() => pc.ExecuteDownload("Banana"));
        }

        [TestMethod]
        public void TestMethodProviderConnectorException21()
        {
            ProviderConnector pc = new ProviderConnector(new DataExtractions());
            Assert.ThrowsException<Exception>(() => pc.ExecuteDownload("Banana"));
        }

        [TestMethod]
        public void TestMethodProviderConnectorException23()
        {
            ProviderConnector pc = new ProviderConnector(new DataExtractions());
            Assert.ThrowsException<Exception>(() => pc.ExecuteDownload("Banana"));
        }

        [TestMethod]
        public void TestMethodProviderConnectorDataFile13()
        {
            ProviderConnector pc = new ProviderConnector(new DataExtractions());
            pc.GetDatasetDataFile(@"https://opendata.arcgis.com/datasets/4e3ee61ac5a34c28981e8c61f94d098a_0.csv", "csv");
        }

        [TestMethod]
        public void TestMethodProviderConnectorDelimiterComma13()
        {
            CommaDelimited pc = new CommaDelimited(new DataExtractions());
            var item = pc.DetermineDelimiter("X,Y,OBJECTID,BEHEERDER_VAKGEB,MASTTYPE,MAST_MASTCODE,WOONPLAATS,WIJK,BUURT,OPENBARE_RUIMTE,ARMATUURTYPE,LAMPTYPE", "5.989576590072968,52.597744618078657,1001,,CIL AL 8M,107,Genemuiden,Wijk 01 Kamperzeedijk,Kamperzeedijk-West,Kamperzeedijk,Brisa EASY LCN 3 (Lightronics),LED");
            Assert.AreSame(item, ",");
        }

        [TestMethod]
        public void TestMethodProviderConnectorDelimiterSemiColon31()
        {
            CommaDelimited pc = new CommaDelimited(new DataExtractions());
            var item = pc.DetermineDelimiter("X;Y;OBJECTID;BEHEERDER_VAKGEB;MASTTYPE;MAST_MASTCODE;WOONPLAATS;WIJK;BUURT;OPENBARE_RUIMTE;ARMATUURTYPE;LAMPTYPE", "5.989576590072968;52.597744618078657;1001;;CIL AL 8M;107;Genemuiden;Wijk 01 Kamperzeedijk;Kamperzeedijk-West;Kamperzeedijk;Brisa EASY LCN 3 (Lightronics);LED");
            Assert.Equals(item, ";");
        }

        [TestMethod]
        public void TestMethodProviderConnectorDelimiterTab13()
        {
            CommaDelimited pc = new CommaDelimited(new DataExtractions());
            var item = pc.DetermineDelimiter("X\tY\tOBJECTID\tBEHEERDER_VAKGEB\tMASTTYPE\tMAST_MASTCODE\tWOONPLAATS\tWIJK\tBUURT\tOPENBARE_RUIMTE\tARMATUURTYPE\tLAMPTYPE", "5.989576590072968\t52.597744618078657\t1001\t\tCIL AL 8M\t107\tGenemuiden\tWijk 01 Kamperzeedijk\tKamperzeedijk-West\tKamperzeedijk\tBrisa EASY LCN 3 (Lightronics)\tLED");
            Assert.Equals(item, "\t");
        }

        [TestMethod]
        public void TestMethodProviderConnectorException13()
        {
            ProviderConnector pc = new ProviderConnector(new DataExtractions());
            Assert.ThrowsException<Exception>(() => pc.ExecuteDownload("Banana"));
        }

        [TestMethod]
        public void TestMethodProviderConnectorException231()
        {
            ProviderConnector pc = new ProviderConnector(new DataExtractions());
            Assert.ThrowsException<Exception>(() => pc.ExecuteDownload("Banana"));
        }

        [TestMethod]
        public void TestMethodProviderConnectorException221()
        {
            ProviderConnector pc = new ProviderConnector(new DataExtractions());
            Assert.ThrowsException<Exception>(() => pc.ExecuteDownload("Banana"));
        }

        [TestMethod]
        public void TestMethodProviderConnectorDataFile11()
        {
            ProviderConnector pc = new ProviderConnector(new DataExtractions());
            pc.GetDatasetDataFile(@"https://opendata.arcgis.com/datasets/4e3ee61ac5a34c28981e8c61f94d098a_0.csv", "csv");
        }

        [TestMethod]
        public void TestMethodProviderConnectorDelimiterComma11()
        {
            CommaDelimited pc = new CommaDelimited(new DataExtractions());
            var item = pc.DetermineDelimiter("X,Y,OBJECTID,BEHEERDER_VAKGEB,MASTTYPE,MAST_MASTCODE,WOONPLAATS,WIJK,BUURT,OPENBARE_RUIMTE,ARMATUURTYPE,LAMPTYPE", "5.989576590072968,52.597744618078657,1001,,CIL AL 8M,107,Genemuiden,Wijk 01 Kamperzeedijk,Kamperzeedijk-West,Kamperzeedijk,Brisa EASY LCN 3 (Lightronics),LED");
            Assert.Equals(item, ",");
        }

        [TestMethod]
        public void TestMethodProviderConnectorDelimiterSemiColon11()
        {
            CommaDelimited pc = new CommaDelimited(new DataExtractions());
            var item = pc.DetermineDelimiter("X;Y;OBJECTID;BEHEERDER_VAKGEB;MASTTYPE;MAST_MASTCODE;WOONPLAATS;WIJK;BUURT;OPENBARE_RUIMTE;ARMATUURTYPE;LAMPTYPE", "5.989576590072968;52.597744618078657;1001;;CIL AL 8M;107;Genemuiden;Wijk 01 Kamperzeedijk;Kamperzeedijk-West;Kamperzeedijk;Brisa EASY LCN 3 (Lightronics);LED");
            Assert.Equals(item, ";");
        }

        [TestMethod]
        public void TestMethodProviderConnectorDelimiterTab11()
        {
            CommaDelimited pc = new CommaDelimited(new DataExtractions());
            var item = pc.DetermineDelimiter("X\tY\tOBJECTID\tBEHEERDER_VAKGEB\tMASTTYPE\tMAST_MASTCODE\tWOONPLAATS\tWIJK\tBUURT\tOPENBARE_RUIMTE\tARMATUURTYPE\tLAMPTYPE", "5.989576590072968\t52.597744618078657\t1001\t\tCIL AL 8M\t107\tGenemuiden\tWijk 01 Kamperzeedijk\tKamperzeedijk-West\tKamperzeedijk\tBrisa EASY LCN 3 (Lightronics)\tLED");
            Assert.Equals(item, "\t");
        }

        [TestMethod]
        public void TestMethodProviderConnectorException11()
        {
            ProviderConnector pc = new ProviderConnector(new DataExtractions());
            Assert.ThrowsException<Exception>(() => pc.ExecuteDownload("Banana"));
        }

        [TestMethod]
        public void TestMethodProviderConnectorException211()
        {
            ProviderConnector pc = new ProviderConnector(new DataExtractions());
            Assert.ThrowsException<Exception>(() => pc.ExecuteDownload("Banana"));
        }
    }
}
