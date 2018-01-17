using System;

namespace Octokit.GraphQL.Core.Generation
{
    public class GeneratedFile: IEquatable<GeneratedFile>
    {
        public GeneratedFile(string path, string content)
        {
            Path = path;
            Content = content;
        }

        public string Path;
        public string Content;

        public bool Equals(GeneratedFile other)
        {
            return other != null
                && Path.Equals(other.Path)
                && Content.Equals(other.Content);
        }
    }
}
