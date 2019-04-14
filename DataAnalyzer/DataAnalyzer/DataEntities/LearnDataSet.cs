using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAnalyzer.DataEntities
{
    public class LearnDataSet
    {
        public string DataPath { get; set; }

        public int RecordCount { get; set; }

        public bool HasHeader { get; set; }

        public string[] InputColumns { get; set; }
    }
}
