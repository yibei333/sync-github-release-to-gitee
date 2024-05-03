using System;
using SyncGithubReleaseToGitee.Handlers;
using SyncGithubReleaseToGitee.Handlers.Common;

namespace SyncGithubReleaseToGitee
{
    internal class Program
    {
        static void Main()
        {
            var parameters = ParameterExtension.GetParameter();
            var context = new HandleContext(parameters);
            new RemoveExistHandle(context).Handle();
            new GetLastReleaseHandle(context).Handle();
            new AddHandle(context).Handle();
            new DownloadFilesHandle(context).Handle();
            new UploadFilesHandle(context).Handle();
            Console.WriteLine("ok");
        }
    }
}
