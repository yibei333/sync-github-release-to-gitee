using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace SyncGithubReleaseToGitee.Handlers.Models
{
    [DataContract]
    public class LastReleaseResponse
    {
        [DataMember(Name = "target_commitish")]
        public string TargetCommitish { get; set; }

        [DataMember(Name = "body")]
        public string Body { get; set; }

        [DataMember(Name = "tag_name")]
        public string TagName { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "assets")]
        public List<LastReleaseAssetsResponse> Assets { get; set; }
    }

    [DataContract]
    public class LastReleaseAssetsResponse
    {
        [DataMember(Name = "browser_download_url")]
        public string Url { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        public string FilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory,Name);
    }
}
