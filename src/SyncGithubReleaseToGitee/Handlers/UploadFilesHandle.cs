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
                        { new StreamContent(File.OpenRead(x.FilePath)), "file",x.FilePath }
                    };
                    var response = client.PostAsync($"https://gitee.com/api/v5/repos/{Context.Parameter.Repo}/releases/{Context.ReleaseId}/attach_files?access_token={Context.Parameter.GiteeToken}", content).GetAwaiter().GetResult();
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"upload file '{x.Name}' failed:{response.StatusCode}");
                        Console.WriteLine($"url:https://gitee.com/api/v5/repos/{Context.Parameter.Repo}/releases/{Context.ReleaseId}/attach_files");
                        throw new Exception($"upload file '{x.Name}' failed");
                    }
                });
            }
        }
    }
}
