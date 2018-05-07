using System;

namespace Octokit.GraphQL.Core
{
    public interface IPageInfo : IQueryableValue<IPageInfo>
    {
        string EndCursor { get; }
        bool HasNextPage { get; }
        bool HasPreviousPage { get; }
        string StartCursor { get; }
    }
}