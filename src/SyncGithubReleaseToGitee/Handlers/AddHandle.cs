using System.Collections.Generic;
using System.Net.Http;
using System;
using SyncGithubReleaseToGitee.Extensions;
using SyncGithubReleaseToGitee.Handlers.Common;
using SyncGithubReleaseToGitee.Handlers.Models;

namespace SyncGithubReleaseToGitee.Handlers
{
    public class AddHandle : BaseHandle
    {
        public AddHandle(HandleContext context) : base(context)
        { }

        protected override void HandleInternal()
        {
            using (var client = new HttpClient())
            {
                var parameters = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("access_token", Context.Parameter.GiteeToken),
                    new KeyValuePair<string, string>("tag_name", Context.LastRelease.TagName),
                    new KeyValuePair<string, string>("name", Context.LastRelease.Name),
                    new KeyValuePair<string, string>("body", Context.LastRelease.Body),
                    new KeyValuePair<string, string>("target_commitish", Context.LastRelease.TargetCommitish),
                };
                var content = new FormUrlEncodedContent(parameters);
                var response = client.PostAsync($" https://gitee.com/api/v5/repos/{Context.Parameter.Owner}/{Context.Parameter.Repo}/releases", content).GetAwaiter().GetResult();
                var json = response.Content?.ReadAsStringAsync()?.GetAwaiter().GetResult();
                if (!response.IsSuccessStatusCode) throw new Exception($"add release failed:{json}");
                Context.ReleaseId = json.Deserialize<IdResponse>().Id;
            }
        }
    }
}
