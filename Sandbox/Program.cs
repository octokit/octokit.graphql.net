using System;
using System.Linq;
using System.Threading.Tasks;
using LinqToGraphQL;
using Octoqit;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            RunRepositoryQuery(args[0]).Wait();
            Console.ReadKey();
        }

        private static async Task RunRepositoryQuery(string token)
        {
            var query = new RootQuery()
                .RepositoryOwner("grokys")
                .Repository("VisualStudio")
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
                });

            var connection = new Connection("https://api.github.com/graphql", token);
            var result = (await connection.Run(query)).Single();

            Console.WriteLine("Id: " + result.Id);
            Console.WriteLine("Name: " + result.Name);
            Console.WriteLine("Owner: " + result.Owner.Single().Login);
            Console.WriteLine("IsFork: " + result.IsFork);
            Console.WriteLine("IsPrivate: " + result.IsPrivate);
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