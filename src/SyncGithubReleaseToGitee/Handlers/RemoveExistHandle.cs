using SyncGithubReleaseToGitee.Extensions;
using SyncGithubReleaseToGitee.Handlers.Common;
using SyncGithubReleaseToGitee.Handlers.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace SyncGithubReleaseToGitee.Handlers
{
    public class RemoveExistHandle : BaseHandle
    {
        public RemoveExistHandle(HandleContext context) : base(context)
        { }

        protected override void HandleInternal()
        {
            using (var client = new HttpClient())
            {
                //1.get all release list
                var response = client.GetAsync($"https://gitee.com/api/v5/repos/{Context.Parameter.Repo}/releases?access_token={Context.Parameter.GiteeToken}").GetAwaiter().GetResult();
                var json = response.Content?.ReadAsStringAsync()?.GetAwaiter().GetResult();
                if (!response.IsSuccessStatusCode) throw new Exception($"get release list failed:{json}");
                var list = json.Deserialize<List<IdResponse>>();

                //2.remove release list
                list.ForEach(x =>
                {
                    var deleteResponse = client.DeleteAsync($"https://gitee.com/api/v5/repos/{Context.Parameter.Repo}/releases/{x.Id}?access_token={Context.Parameter.GiteeToken}").GetAwaiter().GetResult();
                    if (!deleteResponse.IsSuccessStatusCode) throw new Exception($"delete release {x.Id} failed:{deleteResponse.StatusCode}");
                });
            }
        }
    }
}
