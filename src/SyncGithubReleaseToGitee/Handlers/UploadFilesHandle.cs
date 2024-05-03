using SyncGithubReleaseToGitee.Handlers.Common;
using System;
using System.IO;
using System.Net.Http;

namespace SyncGithubReleaseToGitee.Handlers
{
    public class UploadFilesHandle : BaseHandle
    {
        public UploadFilesHandle(HandleContext context) : base(context)
        { }

        protected override void HandleInternal()
        {
            using (var client = new HttpClient())
            {
                Context.LastRelease.Assets.ForEach(x =>
                {
                    var content = new MultipartFormDataContent
                    {
                        { new StringContent(Context.Parameter.GiteeToken), "access_token" },
                        { new StreamContent(new FileStream(x.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read)), "file" }
                    };
                    var response = client.PostAsync($"https://gitee.com/api/v5/repos/{Context.Parameter.Owner}/{Context.Parameter.Repo}/releases/{Context.ReleaseId}/attach_files", content).GetAwaiter().GetResult();
                    if (!response.IsSuccessStatusCode) throw new Exception($"upload file '{x.Name}' failed");
                });
            }
        }
    }
}
