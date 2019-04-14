using DataAnalyzer.DataEntities;

using Microsoft.Extensions.Options;

namespace DataAnalyzer.Tests
{
    public class OptionsMock : IOptions<Settings>
    {
        public Settings Value => new Settings { ConnectionString = "Server=localhost;Database=PBD;Trusted_Connection=True;" };
    }
}
