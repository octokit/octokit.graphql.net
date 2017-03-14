namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Represents a 'deployed' event on a given issue or pull request.
    /// </summary>
    public class DeployedEvent : QueryEntity
    {
        public DeployedEvent(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the actor (user) associated with the event.
        /// </summary>
        public User Actor => this.CreateProperty(x => x.Actor, Octoqit.User.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        /// <summary>
        /// The deployment associated with the 'deployed' event.
        /// </summary>
        public Deployment Deployment => this.CreateProperty(x => x.Deployment, Octoqit.Deployment.Create);

        public string Id { get; }

        /// <summary>
        /// Identifies the issue associated with the event.
        /// </summary>
        public Issue Issue => this.CreateProperty(x => x.Issue, Octoqit.Issue.Create);

        /// <summary>
        /// The ref associated with the 'deployed' event.
        /// </summary>
        public Ref Ref => this.CreateProperty(x => x.Ref, Octoqit.Ref.Create);

        /// <summary>
        /// Identifies the repository associated with the event.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octoqit.Repository.Create);

        /// <summary>
        /// Identifies the event type associated with the event.
        /// </summary>
        public IssueEventType Type { get; }

        internal static DeployedEvent Create(IQueryProvider provider, Expression expression)
        {
            return new DeployedEvent(provider, expression);
        }
    }
}