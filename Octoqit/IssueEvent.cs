namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Represents an issue event.
    /// </summary>
    public interface IIssueEvent : IQueryEntity
    {
        /// <summary>
        /// Identifies the actor (user) associated with the event.
        /// </summary>
        User Actor { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        string CreatedAt { get; }

        /// <summary>
        /// Identifies the issue associated with the event.
        /// </summary>
        Issue Issue { get; }

        /// <summary>
        /// Identifies the repository associated with the event.
        /// </summary>
        Repository Repository { get; }

        /// <summary>
        /// Identifies the event type associated with the event.
        /// </summary>
        IssueEventType Type { get; }
    }
}

namespace Octoqit.Internal
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    internal class StubIIssueEvent : QueryEntity, IIssueEvent
    {
        public StubIIssueEvent(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public User Actor => this.CreateProperty(x => x.Actor, Octoqit.User.Create);

        public string CreatedAt { get; }

        public Issue Issue => this.CreateProperty(x => x.Issue, Octoqit.Issue.Create);

        public Repository Repository => this.CreateProperty(x => x.Repository, Octoqit.Repository.Create);

        public IssueEventType Type { get; }

        internal static StubIIssueEvent Create(IQueryProvider provider, Expression expression)
        {
            return new StubIIssueEvent(provider, expression);
        }
    }
}