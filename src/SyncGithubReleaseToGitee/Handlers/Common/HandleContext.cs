using SyncGithubReleaseToGitee.Handlers.Models;

namespace SyncGithubReleaseToGitee.Handlers.Common
{
    public class HandleContext
    {
        public HandleContext(Parameter parameter)
        {
            Parameter = parameter;
        }

        public Parameter Parameter { get; }
        public LastReleaseResponse LastRelease { get; set; }
        public string Tag { get; set; }
        public string ReleaseId { get; set; }
    }
}
