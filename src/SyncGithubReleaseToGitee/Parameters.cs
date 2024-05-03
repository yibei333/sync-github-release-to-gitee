using System;
using SyncGithubReleaseToGitee.Extensions;

namespace SyncGithubReleaseToGitee
{
    public class Parameter
    {
        public string Repo { get; set; }
        public string Owner { get; set; }
        public string GithubToken { get; set; }
        public string GiteeToken { get; set; }
    }

    public static class ParameterExtension
    {
        public static Parameter GetParameter()
        {
            var parameter = new Parameter
            {
                Repo = Environment.GetEnvironmentVariable("repo") ?? throw new ArgumentException("'repo' parameter required"),
                Owner = Environment.GetEnvironmentVariable("owner") ?? throw new ArgumentException("'owner' parameter required"),
                GiteeToken = Environment.GetEnvironmentVariable("gitee_token") ?? throw new ArgumentException("'gitee_token' parameter required"),
                GithubToken = Environment.GetEnvironmentVariable("github_token") ?? throw new ArgumentException("'github_token' parameter required"),
            };
            Console.WriteLine(parameter.Serialize());
            return parameter;
        }
    }
}

