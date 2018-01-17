using System;

namespace Octokit.GraphQL.Core.Generation
{
    public struct GeneratedFile
    {
        public GeneratedFile(string path, string content)
        {
            Path = path;
            Content = content;
        }

        public string Path;
        public string Content;
    }
}
