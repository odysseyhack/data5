using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAnalyzer.DataEntities
{
    public class MatchResponse
    {
        public bool Matched { get; set; }

        public List<int> Id { get; set; }
    }
}
