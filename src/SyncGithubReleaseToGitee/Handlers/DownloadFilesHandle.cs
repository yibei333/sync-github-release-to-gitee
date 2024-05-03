using SyncGithubReleaseToGitee.Handlers.Common;
using System;
using System.IO;
using System.Net.Http;

namespace SyncGithubReleaseToGitee.Handlers
{
    public class DownloadFilesHandle : BaseHandle
    {
        public DownloadFilesHandle(HandleContext context) : base(context)
        { }

        protected override void HandleInternal()
        {
            using (var client = new HttpClient())
            {
                Context.LastRelease.Assets.ForEach(x =>
                {
                    Console.WriteLine($"downloading file {x.Name}");
                    var fileStream = new FileStream(x.FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    var stream = client.GetStreamAsync(x.Url).GetAwaiter().GetResult();
                    stream.CopyTo(fileStream);
                    fileStream.Flush();
                    fileStream.Close();
                    fileStream.Dispose();
                    stream.Close();
                    stream.Dispose();
                    Console.WriteLine($"download file {x.Name} complete");
                });
            }
        }
    }
}
