using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

static class Downloader
{
    static Downloader()
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
    }

    static HttpClient httpClient = new HttpClient(new HttpClientHandler
    {
        AllowAutoRedirect = false,
    })
    {
        Timeout = TimeSpan.FromSeconds(30),
    };

    public static async Task DownloadFile(string targetPath, string requestUri)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Head, requestUri);

        DateTime remoteLastModified;
        using (var headResponse = await httpClient.SendAsync(requestMessage))
        {
            if (headResponse.ReasonPhrase == "Redirect")
            {
                File.Delete(targetPath);
                return;
            }

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
        using (var fileStream = new FileStream(targetPath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            await httpStream.CopyToAsync(fileStream);
        }

        File.SetLastWriteTimeUtc(targetPath, remoteLastModified);
    }
}