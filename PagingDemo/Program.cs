using System;
using System.Threading.Tasks;
using Octokit.GraphQL;
using PagingDemo.Models;

namespace PagingDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await ManualPaging.Run();
            Console.ReadKey();
        }
    }
}
