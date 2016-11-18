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
            RunViewerQuery().Wait();
        }

        private static async Task RunRepositoryQuery()
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

            var connection = new Connection("https://api.github.com/graphql", "3e4cdca2928a89fd9cee364c2055129d06eac6b3");
            var result = await connection.Run(query);

            Console.WriteLine("Id: " + result.Id);
            Console.WriteLine("Name: " + result.Name);
            Console.WriteLine("Owner: " + result.Owner.Single().Login);
            Console.WriteLine("IsFork: " + result.IsFork);
            Console.WriteLine("IsPrivate: " + result.IsPrivate);
        }

        private static async Task RunViewerQuery()
        {
            var query = new RootQuery().Viewer.Select(x => new { x.Login, x.Email });
            var connection = new Connection("https://api.github.com/graphql", "3e4cdca2928a89fd9cee364c2055129d06eac6b3");
            var result = await connection.Run(query);

            Console.WriteLine("Login: " + result.Login);
            Console.WriteLine("Email: " + result.Email);
        }
    }
}