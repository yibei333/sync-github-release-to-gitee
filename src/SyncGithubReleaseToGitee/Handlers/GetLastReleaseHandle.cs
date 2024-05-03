using SyncGithubReleaseToGitee.Extensions;
using SyncGithubReleaseToGitee.Handlers.Common;
using SyncGithubReleaseToGitee.Handlers.Models;
using System;

namespace SyncGithubReleaseToGitee.Handlers
{
    public class GetLastReleaseHandle : BaseHandle
    {
        public GetLastReleaseHandle(HandleContext context) : base(context)
        { }

        protected override void HandleInternal()
        {
            using (var client = Context.CreateGithubHttpClient())
            {
                var response = client.GetAsync($"https://api.github.com/repos/{Context.Parameter.Repo}/releases/latest").GetAwaiter().GetResult();
                var json = response.Content?.ReadAsStringAsync()?.GetAwaiter().GetResult();
                if (!response.IsSuccessStatusCode) throw new Exception($"get last release failed:{json}");
                Context.LastRelease = json.Deserialize<LastReleaseResponse>();
            }
        }
    }
}
