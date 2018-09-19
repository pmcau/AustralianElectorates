using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

static class Downloader
{
    static HttpClient httpClient = new HttpClient
    {
        Timeout = TimeSpan.FromSeconds(30)
    };

    public static async Task DownloadFile(string targetPath, string requestUri)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Head, requestUri);

        DateTime remoteLastModified;
        using (var headResponse = await httpClient.SendAsync(requestMessage))
        {
            remoteLastModified = headResponse.Content.Headers.LastModified.Value.UtcDateTime;
        }

        if (File.Exists(targetPath))
        {
            if (remoteLastModified <= File.GetLastWriteTimeUtc(targetPath))
            {
                return;
            }

            File.Delete(targetPath);
        }

        using (var response = await httpClient.GetAsync(requestUri))
        using (var httpStream = await response.Content.ReadAsStreamAsync())
        {
            using (var fileStream = new FileStream(targetPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await httpStream.CopyToAsync(fileStream);
            }

            File.SetLastWriteTimeUtc(targetPath, remoteLastModified);
        }
    }
}