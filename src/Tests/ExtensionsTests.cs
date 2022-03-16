﻿public class ExtensionsTests
{
    [Theory]
    [InlineData("McLACHLAN, Craig Steven", "McLachlan, Craig Steven")]
    [InlineData("Duncan-Hughes", "Duncan-Hughes")]
    [InlineData("O'BRIEN", "O'Brien")]
    public void ToTitleCase(string input, string expected) =>
        Assert.Equal(expected, input.ToTitleCase());
}