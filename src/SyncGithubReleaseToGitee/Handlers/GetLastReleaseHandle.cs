using SyncGithubReleaseToGitee.Extensions;
using SyncGithubReleaseToGitee.Handlers.Common;
using SyncGithubReleaseToGitee.Handlers.Models;
using System;
using System.Net.Http;

namespace SyncGithubReleaseToGitee.Handlers
{
    public class GetLastReleaseHandle : BaseHandle
    {
        public GetLastReleaseHandle(HandleContext context) : base(context)
        { }

        protected override void HandleInternal()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Context.Parameter.GithubToken);
                var response = client.GetAsync($"https://api.github.com/repos/{Context.Parameter.Repo}/releases/latest").GetAwaiter().GetResult();
                var json = response.Content?.ReadAsStringAsync()?.GetAwaiter().GetResult();
                if (!response.IsSuccessStatusCode) throw new Exception($"get last release failed:{json}");
                Context.LastRelease = json.Deserialize<LastReleaseResponse>();
            }
        }
    }
}
