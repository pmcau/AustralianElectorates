using System.Linq;
using ObjectApproval;
using Xunit;
using Xunit.Abstractions;

public class ElectedParserTests :
    XunitLoggingBase
{
    [Fact]
    public void Run()
    {
        ObjectApprover.VerifyWithJson(ElectedParser.Read("CsvParsers/electedSample.csv").Take(10));
    }

    public ElectedParserTests(ITestOutputHelper output) :
        base(output)
    {
    }
}