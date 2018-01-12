using System;
using System.Threading.Tasks;
using Octokit.GraphQL;
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
        }

        private static async Task GenerateEntities(string token, string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var modelPath = Path.Combine(path, "Model");
            if (!Directory.Exists(modelPath))
            {
                Directory.CreateDirectory(modelPath);
            }

            var header = new ProductHeaderValue("Octokit.GraphQL", "0.1");
            var connection = new Connection(header, token);

            Console.WriteLine("Reading from " + connection.Uri);
            var schema = await SchemaReader.ReadSchema(connection);

            foreach (var file in CodeGenerator.Generate(schema, "Octokit.GraphQL", "Octokit.GraphQL.Model"))
            {
                Console.WriteLine("Writing " + file.FileName);
                File.WriteAllText(Path.Combine(path, file.FileName), file.Content);
            }
        }
    }
}