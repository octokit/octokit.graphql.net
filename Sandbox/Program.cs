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
            RunQuery().Wait();
        }

        private static async Task RunQuery()
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
                        o.AvatarURL,
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
    }
}
