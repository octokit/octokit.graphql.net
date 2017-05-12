namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'milestoned' event on a given issue or pull request.
    /// </summary>
    public class MilestonedEvent : QueryEntity
    {
        public MilestonedEvent(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the 'milestoned' event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        public string Id { get; }

        /// <summary>
        /// Identifies the milestone title associated with the 'milestoned' event.
        /// </summary>
        public string MilestoneTitle { get; }

        /// <summary>
        /// Object referenced by event.
        /// </summary>
        public MilestoneItem Subject => this.CreateProperty(x => x.Subject, Octokit.GraphQL.MilestoneItem.Create);

        internal static MilestonedEvent Create(IQueryProvider provider, Expression expression)
        {
            return new MilestonedEvent(provider, expression);
        }
    }
}