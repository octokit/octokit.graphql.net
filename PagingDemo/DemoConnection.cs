using System;
using System.Threading.Tasks;
using Octokit.GraphQL;

namespace PagingDemo
{
    class DemoConnection : Connection
    {
        public DemoConnection()
            : base(
                new ProductHeaderValue("PagingDemo", "0.0"),
                Environment.GetEnvironmentVariable("OCTOKIT_GQL_OAUTHTOKEN"))
        {
        }

        public override Task<string> Run(string query)
        {
            Console.WriteLine(query);
            Console.WriteLine("================");
            return base.Run(query);
        }
    }
}
