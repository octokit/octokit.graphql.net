using System;

namespace Octokit.GraphQL.Core.Generation
{
    public class GeneratedFile
    {
        public GeneratedFile(string fileName, string content)
        {
            FileName = fileName;
            Content = content;
        }

        public string FileName { get; }
        public string Content { get; }
    }
}
