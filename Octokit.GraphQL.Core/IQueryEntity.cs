using System.Linq;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core
{
    public interface IQueryEntity
    {
        Expression Expression { get; }
        IQueryProvider Provider { get; }
    }
}