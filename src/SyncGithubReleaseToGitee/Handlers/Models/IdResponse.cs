using System.Runtime.Serialization;

namespace SyncGithubReleaseToGitee.Handlers.Models
{
    [DataContract]
    public class IdResponse
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
    }
}
