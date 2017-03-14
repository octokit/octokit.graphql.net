namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Represents a 'base_ref_force_pushed' event on a given pull request.
    /// </summary>
    public class BaseRefForcePushedEvent : QueryEntity
    {
        public BaseRefForcePushedEvent(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the actor (user) associated with the event.
        /// </summary>
        public User Actor => this.CreateProperty(x => x.Actor, Octoqit.User.Create);

        /// <summary>
        /// Identifies the after commit SHA for the 'base_ref_force_pushed' event.
        /// </summary>
        public Commit AfterCommit => this.CreateProperty(x => x.AfterCommit, Octoqit.Commit.Create);

        /// <summary>
        /// Identifies the before commit SHA for the 'base_ref_force_pushed' event.
        /// </summary>
        public Commit BeforeCommit => this.CreateProperty(x => x.BeforeCommit, Octoqit.Commit.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        public string Id { get; }

        /// <summary>
        /// Identifies the issue associated with the event.
        /// </summary>
        public Issue Issue => this.CreateProperty(x => x.Issue, Octoqit.Issue.Create);

        /// <summary>
        /// Identifies the fully qualified ref name for the 'base_ref_force_pushed' event.
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

        internal static BaseRefForcePushedEvent Create(IQueryProvider provider, Expression expression)
        {
            return new BaseRefForcePushedEvent(provider, expression);
        }
    }
}