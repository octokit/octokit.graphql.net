namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// A group of emoji reactions to a particular piece of content.
    /// </summary>
    public class ReactionGroup : QueryEntity
    {
        public ReactionGroup(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the emoji reaction.
        /// </summary>
        public ReactionContent Content { get; }

        /// <summary>
        /// Identifies when the reaction was created.
        /// </summary>
        public string CreatedAt { get; }

        /// <summary>
        /// The subject that was reacted to.
        /// </summary>
        public IReactable Subject => this.CreateProperty(x => x.Subject, Octoqit.Internal.StubIReactable.Create);

        /// <summary>
        /// Users who have reacted to the reaction subject with the emotion represented by this reaction group
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public ReactingUserConnection Users(int? first, string after, int? last, string before) => this.CreateMethodCall(x => x.Users(first, after, last, before), Octoqit.ReactingUserConnection.Create);

        /// <summary>
        /// Whether or not the authenticated user has left a reaction on the subject.
        /// </summary>
        public bool ViewerHasReacted { get; }

        internal static ReactionGroup Create(IQueryProvider provider, Expression expression)
        {
            return new ReactionGroup(provider, expression);
        }
    }
}