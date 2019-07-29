using System.Linq;
using ObjectApproval;
using Xunit;
using Xunit.Abstractions;

public class TwoPartyPreferredTests :
    XunitLoggingBase
{
    [Fact]
    public void Run()
    {
        ObjectApprover.VerifyWithJson(TwoPartyPreferredParser.Read("CsvParsers/TwoPartyPreferredSample.csv").Take(10));
    }

    public TwoPartyPreferredTests(ITestOutputHelper output) :
        base(output)
    {
    }
}