
using System.Collections.Generic;

namespace MachineLearning.V1.Controllers.Model
{
    public class MatchResponse
    {
        public bool Matched { get; set; }

        public List<int> Id { get; set; }
    }
}
