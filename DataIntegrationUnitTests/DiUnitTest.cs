using DatasetDownloader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataIntegrationUnitTests
{
    [TestClass]
    public class DiUnitTest
    {
        [TestMethod]
        public void TestMethodProviderConnectorDataFile()
        {
            ProviderConnector pc = new ProviderConnector();
            pc.GetDatasetDataFile(@"https://opendata.arcgis.com/datasets/4e3ee61ac5a34c28981e8c61f94d098a_0.csv", "csv");
        }

        [TestMethod]
        public void TestMethodProviderConnectorDelimiterComma()
        {
            ProviderConnector pc = new ProviderConnector();
            var item = pc.DetermineDelimiter("X,Y,OBJECTID,BEHEERDER_VAKGEB,MASTTYPE,MAST_MASTCODE,WOONPLAATS,WIJK,BUURT,OPENBARE_RUIMTE,ARMATUURTYPE,LAMPTYPE", "5.989576590072968,52.597744618078657,1001,,CIL AL 8M,107,Genemuiden,Wijk 01 Kamperzeedijk,Kamperzeedijk-West,Kamperzeedijk,Brisa EASY LCN 3 (Lightronics),LED");
            Assert.Equals(item, ",");
        }

        [TestMethod]
        public void TestMethodProviderConnectorDelimiterSemiColon()
        {
            ProviderConnector pc = new ProviderConnector();
            var item = pc.DetermineDelimiter("X;Y;OBJECTID;BEHEERDER_VAKGEB;MASTTYPE;MAST_MASTCODE;WOONPLAATS;WIJK;BUURT;OPENBARE_RUIMTE;ARMATUURTYPE;LAMPTYPE", "5.989576590072968;52.597744618078657;1001;;CIL AL 8M;107;Genemuiden;Wijk 01 Kamperzeedijk;Kamperzeedijk-West;Kamperzeedijk;Brisa EASY LCN 3 (Lightronics);LED");
            Assert.Equals(item, ";");
        }

        [TestMethod]
        public void TestMethodProviderConnectorDelimiterTab()
        {
            ProviderConnector pc = new ProviderConnector();
            var item = pc.DetermineDelimiter("X\tY\tOBJECTID\tBEHEERDER_VAKGEB\tMASTTYPE\tMAST_MASTCODE\tWOONPLAATS\tWIJK\tBUURT\tOPENBARE_RUIMTE\tARMATUURTYPE\tLAMPTYPE", "5.989576590072968\t52.597744618078657\t1001\t\tCIL AL 8M\t107\tGenemuiden\tWijk 01 Kamperzeedijk\tKamperzeedijk-West\tKamperzeedijk\tBrisa EASY LCN 3 (Lightronics)\tLED");
            Assert.Equals(item, "\t");
        }

        [TestMethod]
        public void TestMethodProviderConnectorException()
        {
            ProviderConnector pc = new ProviderConnector();
            Assert.ThrowsException<Exception>(() => pc.ExecuteDownload("Banana"));
        }

        [TestMethod]
        public void TestMethodProviderConnectorException2()
        {
            ProviderConnector pc = new ProviderConnector();
            Assert.ThrowsException<Exception>(() => pc.ExecuteDownload("Banana"));
        }
    }
}
