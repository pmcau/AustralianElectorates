using CaptureSnippets;
using Xunit;

public class DocoUpdater
{
    [Fact]
    public void Run()
    {
        var root = GitRepoDirectoryFinder.FindForFilePath();
        DirectorySourceMarkdownProcessor.Run(root);
    }
}