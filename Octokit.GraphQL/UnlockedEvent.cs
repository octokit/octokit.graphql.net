namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'unlocked' event on a given issue or pull request.
    /// </summary>
    public class UnlockedEvent : QueryEntity
    {
        public UnlockedEvent(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the 'unlocked' event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        public string Id { get; }

        /// <summary>
        /// Object that was unlocked.
        /// </summary>
        public ILockable Lockable => this.CreateProperty(x => x.Lockable, Octokit.GraphQL.Internal.StubILockable.Create);

        internal static UnlockedEvent Create(IQueryProvider provider, Expression expression)
        {
            return new UnlockedEvent(provider, expression);
        }
    }
}