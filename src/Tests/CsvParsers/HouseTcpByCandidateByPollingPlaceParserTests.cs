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
        ObjectApprover.Verify(HouseTcpByCandidateByPollingPlaceParser.Read("CsvParsers/HouseTcpByCandidateByPollingPlaceSample.csv").Take(10));
    }
    [Fact]
    public void ReadPaired()
    {
        HouseTcpByCandidateByPollingPlaceParser.ReadPaired("CsvParsers/HouseTcpByCandidateByPollingPlaceSample.csv");
    }

    public HouseTcpByCandidateByPollingPlaceParserTests(ITestOutputHelper output) :
        base(output)
    {
    }
}