using System;

namespace LinqToGraphQL.Generation
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
