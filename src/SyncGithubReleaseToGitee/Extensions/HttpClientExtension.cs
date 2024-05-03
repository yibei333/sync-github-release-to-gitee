using SyncGithubReleaseToGitee.Handlers.Common;
using System.Net.Http;

namespace SyncGithubReleaseToGitee.Extensions
{
    public static class HttpClientExtension
    {
        public static HttpClient CreateGithubHttpClient(this HandleContext context)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36 Edg/124.0.0.0");
            client.DefaultRequestHeaders.Add("Authorization", context.Parameter.GithubToken);
            return client;
        }

        public static HttpClient CreateGiteeHttpClient(this HandleContext context)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36 Edg/124.0.0.0");
            return client;
        }
    }
}
