using System;
using System.Threading.Tasks;

namespace PagingDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await AutoPaging.Run();
            Console.ReadKey();
        }
    }
}
