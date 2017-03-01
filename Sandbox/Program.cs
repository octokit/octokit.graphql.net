using System;
using System.Linq;
using System.Threading.Tasks;
using LinqToGraphQL;
using LinqToGraphQL.Generation;
using Octoqit;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateEntities(args[0]).Wait();
            //RunRepositoryQuery(args[0]).Wait();
            Console.ReadKey();
        }

        private static async Task GenerateEntities(string token)
        {
            var connection = new Connection("https://api.github.com/graphql", token);
            var schema = await SchemaReader.ReadSchema(connection);

            foreach (var file in CodeGenerator.Generate(schema, "Octoqit"))
            {
                Console.WriteLine(file);
            }
        }

        private static async Task RunRepositoryQuery(string token)
        {
            var query = new RootQuery()
                .Select(root => root
                    .RepositoryOwner("grokys")
                    .Repositories(10, null, null, null)
                    .Edges.Select(x => x.Node)
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        Owner = x.Owner.Select(o => new
                        {
                            o.Login,
                            o.AvatarUrl,
                        }),
                        x.IsFork,
                        x.IsPrivate,
                        root.Viewer.Login,
                    }));

            var connection = new Connection("https://api.github.com/graphql", token);
            var result = (await connection.Run(query));

            foreach (var i in result.SelectMany(x => x))
            {
                Console.WriteLine("Id: " + i.Id);
                Console.WriteLine("Name: " + i.Name);
                Console.WriteLine("Owner: " + i.Owner.Single().Login);
                Console.WriteLine("IsFork: " + i.IsFork);
                Console.WriteLine("IsPrivate: " + i.IsPrivate);
                Console.WriteLine("Viewer Login: " + i.Login);
            }
        }

        private static async Task RunViewerQuery(string token)
        {
            var query = new RootQuery().Viewer.Select(x => new { x.Login, x.Email });
            var connection = new Connection("https://api.github.com/graphql", token);
            var result = (await connection.Run(query)).Single();

            Console.WriteLine("Login: " + result.Login);
            Console.WriteLine("Email: " + result.Email);
        }
    }
}