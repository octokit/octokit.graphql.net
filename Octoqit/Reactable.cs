namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Represents a subject that can be reacted on.
    /// </summary>
    public interface IReactable : IQueryEntity
    {
        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        int? DatabaseId { get; }

        string Id { get; }

        /// <summary>
        /// Are reaction live updates enabled for this subject.
        /// </summary>
        bool LiveReactionUpdatesEnabled { get; }

        /// <summary>
        /// A list of reactions grouped by content left on the subject.
        /// </summary>
        IQueryable<ReactionGroup> ReactionGroups { get; }

        /// <summary>
        /// A list of Reactions left on the Issue.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="content">Allows filtering Reactions by emoji.</param>
        /// <param name="orderBy">Allows specifying the order in which reactions are returned.</param>
        ReactionConnection Reactions(int? first, string after, int? last, string before, ReactionContent? content, ReactionOrder orderBy);

        /// <summary>
        /// The websocket channel ID for reaction live updates.
        /// </summary>
        string ReactionsWebsocket { get; }

        /// <summary>
        /// The repository associated with this reaction subject.
        /// </summary>
        Repository Repository { get; }

        /// <summary>
        /// Can user react to this subject
        /// </summary>
        bool ViewerCanReact { get; }
    }
}

namespace Octoqit.Internal
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    internal class StubIReactable : QueryEntity, IReactable
    {
        public StubIReactable(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public int? DatabaseId { get; }

        public string Id { get; }

        public bool LiveReactionUpdatesEnabled { get; }

        public IQueryable<ReactionGroup> ReactionGroups => this.CreateProperty(x => x.ReactionGroups);

        public ReactionConnection Reactions(int? first, string after, int? last, string before, ReactionContent? content, ReactionOrder orderBy) => this.CreateMethodCall(x => x.Reactions(first, after, last, before, content, orderBy), Octoqit.ReactionConnection.Create);

        public string ReactionsWebsocket { get; }

        public Repository Repository => this.CreateProperty(x => x.Repository, Octoqit.Repository.Create);

        public bool ViewerCanReact { get; }

        internal static StubIReactable Create(IQueryProvider provider, Expression expression)
        {
            return new StubIReactable(provider, expression);
        }
    }
}