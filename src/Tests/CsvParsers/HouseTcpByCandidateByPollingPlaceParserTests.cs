using System.Linq;
using ObjectApproval;
using Xunit;
using Xunit.Abstractions;

public class HouseTcpByCandidateByPollingPlaceParserTests :
    XunitLoggingBase
{
    [Fact]
    public void Run()
    {
        ObjectApprover.VerifyWithJson(HouseTcpByCandidateByPollingPlaceParser.Read("CsvParsers/HouseTcpByCandidateByPollingPlaceSample.csv").Take(10));
    }

    public HouseTcpByCandidateByPollingPlaceParserTests(ITestOutputHelper output) :
        base(output)
    {
    }
}