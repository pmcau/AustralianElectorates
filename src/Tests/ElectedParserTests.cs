using ObjectApproval;
using Xunit;
using Xunit.Abstractions;

public class ElectedParserTests :
    XunitLoggingBase
{
    [Fact]
    public void Run()
    {
        ObjectApprover.VerifyWithJson(ElectedParser.Read("electedSample.csv"));
    }

    public ElectedParserTests(ITestOutputHelper output) :
        base(output)
    {
    }
}