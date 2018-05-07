using System;
using System.Threading.Tasks;
using Octokit.GraphQL.Core.UnitTests.Models;
using Xunit;

namespace Octokit.GraphQL.Core.UnitTests
{
    public class PagingTests
    {
        [Fact]
        public void Foo()
        {
            var query = new Query()
                .Repository("octokit", "octokit.net")
                .Issues()
                .AllPages()
                .Select(issue => new
                {
                    issue.Number,
                }).Compile();
        }
    }
}
