namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

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
        ReactionConnection Reactions(int? first = null, string after = null, int? last = null, string before = null, ReactionContent? content = null, ReactionOrder orderBy = null);

        /// <summary>
        /// Can user react to this subject
        /// </summary>
        bool ViewerCanReact { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIReactable : QueryEntity, IReactable
    {
        public StubIReactable(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        [Obsolete(@"Exposed database IDs will eventually be removed in favor of global Relay IDs.")]
        public int? DatabaseId { get; }

        public string Id { get; }

        public IQueryable<ReactionGroup> ReactionGroups => this.CreateProperty(x => x.ReactionGroups);

        public ReactionConnection Reactions(int? first = null, string after = null, int? last = null, string before = null, ReactionContent? content = null, ReactionOrder orderBy = null) => this.CreateMethodCall(x => x.Reactions(first, after, last, before, content, orderBy), Octokit.GraphQL.Model.ReactionConnection.Create);

        public bool ViewerCanReact { get; }

        internal static StubIReactable Create(IQueryProvider provider, Expression expression)
        {
            return new StubIReactable(provider, expression);
        }
    }
}