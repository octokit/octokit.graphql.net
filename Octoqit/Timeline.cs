namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Represents all of the events visible to a user from an Issue or PullRequest timeline.
    /// </summary>
    public interface ITimeline : IQueryEntity
    {
        /// <summary>
        /// A list of events associated with an Issue or PullRequest.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="since">Allows filtering timeline events by a `since` timestamp.</param>
        IssueTimelineConnection Timeline(int? first, string after, int? last, string before, string since);
    }
}

namespace Octoqit.Internal
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    internal class StubITimeline : QueryEntity, ITimeline
    {
        public StubITimeline(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public IssueTimelineConnection Timeline(int? first, string after, int? last, string before, string since) => this.CreateMethodCall(x => x.Timeline(first, after, last, before, since), Octoqit.IssueTimelineConnection.Create);

        internal static StubITimeline Create(IQueryProvider provider, Expression expression)
        {
            return new StubITimeline(provider, expression);
        }
    }
}