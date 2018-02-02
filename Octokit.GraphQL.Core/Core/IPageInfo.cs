namespace Octokit.GraphQL.Core
{
    public interface IPageInfo
    {
        string EndCursor { get; }
        bool HasNextPage { get; }
        bool HasPreviousPage { get; }
        string StartCursor { get; }
    }
}