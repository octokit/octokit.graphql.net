namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

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

namespace Octokit.GraphQL.Internal
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIIssueEvent : QueryEntity, IIssueEvent
    {
        public StubIIssueEvent(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public User Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.User.Create);

        public string CreatedAt { get; }

        public Issue Issue => this.CreateProperty(x => x.Issue, Octokit.GraphQL.Issue.Create);

        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Repository.Create);

        public IssueEventType Type { get; }

        internal static StubIIssueEvent Create(IQueryProvider provider, Expression expression)
        {
            return new StubIIssueEvent(provider, expression);
        }
    }
}