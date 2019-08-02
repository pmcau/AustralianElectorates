using Xunit;
using Xunit.Abstractions;

public class ExtensionsTests :
    XunitLoggingBase
{
    [Theory]
    [InlineData("McLACHLAN, Craig Steven", "McLachlan, Craig Steven")]
    public void ToTitleCase(string input, string expected)
    {
        Assert.Equal(expected, input.ToTitleCase());
    }

    public ExtensionsTests(ITestOutputHelper output) :
        base(output)
    {
    }
}