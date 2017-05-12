using System;
using System.Threading.Tasks;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Core.Generation;
using System.IO;

namespace Generate
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("GitHub GraphQL .NET entity generator");
                Console.WriteLine("usage: Generate.exe [apitoken] [path]");
                return;
            }

            GenerateEntities(args[0], args[1]).Wait();
            Console.ReadKey();
        }

        private static async Task GenerateEntities(string token, string path)
        {
            var url = "https://api.github.com/graphql";
            var connection = new Connection(url, token);

            Console.WriteLine("Reading from " + url);
            var schema = await SchemaReader.ReadSchema(connection);

            foreach (var file in CodeGenerator.Generate(schema, "Octokit.GraphQL"))
            {
                Console.WriteLine("Writing " + file.FileName);
                File.WriteAllText(Path.Combine(path, file.FileName), file.Content);
            }
        }
    }
}