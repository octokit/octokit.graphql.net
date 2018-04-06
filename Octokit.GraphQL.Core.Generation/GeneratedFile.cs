using System;
using IoPath = System.IO.Path;

namespace Octokit.GraphQL.Core.Generation
{
    public class GeneratedFile
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
