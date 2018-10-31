namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A Gist.
    /// </summary>
    public class Gist : QueryableValue<Gist>
    {
        public Gist(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of comments associated with the gist
        /// </summary>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        public GistCommentConnection Comments(Arg<string>? after = null, Arg<string>? before = null, Arg<int>? first = null, Arg<int>? last = null) => this.CreateMethodCall(x => x.Comments(after, before, first, last), Octokit.GraphQL.Model.GistCommentConnection.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The gist description.
        /// </summary>
        public string Description { get; }

        public ID Id { get; }

        /// <summary>
        /// Whether the gist is public or not.
        /// </summary>
        public bool IsPublic { get; }

        /// <summary>
        /// The gist name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The gist owner.
        /// </summary>
        public IRepositoryOwner Owner => this.CreateProperty(x => x.Owner, Octokit.GraphQL.Model.Internal.StubIRepositoryOwner.Create);

        /// <summary>
        /// Identifies when the gist was last pushed to.
        /// </summary>
        public DateTimeOffset? PushedAt { get; }

        /// <summary>
        /// A list of users who have starred this starrable.
        /// </summary>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="orderBy">Order for connection</param>
        public StargazerConnection Stargazers(Arg<string>? after = null, Arg<string>? before = null, Arg<int>? first = null, Arg<int>? last = null, Arg<StarOrder>? orderBy = null) => this.CreateMethodCall(x => x.Stargazers(after, before, first, last, orderBy), Octokit.GraphQL.Model.StargazerConnection.Create);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// Returns a boolean indicating whether the viewing user has starred this starrable.
        /// </summary>
        public bool ViewerHasStarred { get; }

        internal static Gist Create(Expression expression)
        {
            return new Gist(expression);
        }
    }
}