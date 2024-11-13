using System.Net;

static class Downloader
{
    static Downloader() =>
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
                                               SecurityProtocolType.Tls11 |
                                               SecurityProtocolType.Tls12;

    static HttpClient httpClient = new(new HttpClientHandler
    {
        AllowAutoRedirect = true
    })
    {
        Timeout = TimeSpan.FromSeconds(30)
    };

    public static async Task<string> DownloadFile(string targetPath, string requestUri)
    {
        try
        {
            for (var i = 0; i < 10; i++)
            {
                try
                {
                    return await InnerDownload(targetPath, requestUri);
                }
                catch
                {
                    await Task.Delay(1000);
                    File.Delete(targetPath);
                }
            }
        }
        catch (Exception exception)
        {
            throw new(
                $"""
                 Download failed:
                   Path: {targetPath}
                   Uri: {requestUri}
                 """,
                exception);
        }
        throw new(
            $"""
             Download failed:
               Path: {targetPath}
               Uri: {requestUri}
             """);
    }

    static async Task<string> InnerDownload(string targetPath, string requestUri)
    {
        using (var response = await httpClient.GetAsync(requestUri))
        {
            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                requestUri = response.Headers.Location!.ToString();
            }
        }

        DateTime remoteLastModified;
        using (var response = await httpClient.GetAsync(requestUri))
        {
            remoteLastModified = response.Content.Headers.LastModified.GetValueOrDefault(DateTimeOffset.UtcNow)
                .UtcDateTime;
            if (File.Exists(targetPath))
            {
                if (remoteLastModified <= File.GetLastWriteTimeUtc(targetPath))
                {
                    return requestUri;
                }

                File.Delete(targetPath);
            }

            await using var httpStream = await response.Content.ReadAsStreamAsync();
            await using var fileStream = new FileStream(targetPath, FileMode.Create, FileAccess.Write, FileShare.None);
            await httpStream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
        }

        File.SetLastWriteTimeUtc(targetPath, remoteLastModified);
        return requestUri;
    }
}