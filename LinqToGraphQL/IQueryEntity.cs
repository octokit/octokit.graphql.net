using System.Linq;
using System.Linq.Expressions;

namespace LinqToGraphQL
{
    public interface IQueryEntity
    {
        Expression Expression { get; }
        IQueryProvider Provider { get; }
    }
}