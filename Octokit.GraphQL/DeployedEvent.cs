namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'deployed' event on a given pull request.
    /// </summary>
    public class DeployedEvent : QueryEntity
    {
        public DeployedEvent(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the 'deployed' event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        /// <summary>
        /// The deployment associated with the 'deployed' event.
        /// </summary>
        public Deployment Deployment => this.CreateProperty(x => x.Deployment, Octokit.GraphQL.Deployment.Create);

        public string Id { get; }

        /// <summary>
        /// PullRequest referenced by event.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.PullRequest.Create);

        /// <summary>
        /// The ref associated with the 'deployed' event.
        /// </summary>
        public Ref Ref => this.CreateProperty(x => x.Ref, Octokit.GraphQL.Ref.Create);

        /// <summary>
        /// The integration that created this object.
        /// </summary>
        public Integration ViaIntegration => this.CreateProperty(x => x.ViaIntegration, Octokit.GraphQL.Integration.Create);

        internal static DeployedEvent Create(IQueryProvider provider, Expression expression)
        {
            return new DeployedEvent(provider, expression);
        }
    }
}