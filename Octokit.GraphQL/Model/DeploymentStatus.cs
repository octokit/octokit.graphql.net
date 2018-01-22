namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Describes the status of a given deployment attempt.
    /// </summary>
    public class DeploymentStatus : QueryableValue<DeploymentStatus>
    {
        public DeploymentStatus(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset? CreatedAt { get; }

        /// <summary>
        /// Identifies the actor who triggered the deployment.
        /// </summary>
        public IActor Creator => this.CreateProperty(x => x.Creator, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the deployment associated with status.
        /// </summary>
        public Deployment Deployment => this.CreateProperty(x => x.Deployment, Octokit.GraphQL.Model.Deployment.Create);

        /// <summary>
        /// Identifies the description of the deployment.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Identifies the environment URL of the deployment.
        /// </summary>
        public string EnvironmentUrl { get; }

        public string Id { get; }

        /// <summary>
        /// Identifies the log URL of the deployment.
        /// </summary>
        public string LogUrl { get; }

        /// <summary>
        /// Identifies the current state of the deployment.
        /// </summary>
        public DeploymentStatusState State { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        [Obsolete(@"General type updated timestamps will eventually be replaced by other field specific timestamps.")]
        public DateTimeOffset? UpdatedAt { get; }

        internal static DeploymentStatus Create(IQueryProvider provider, Expression expression)
        {
            return new DeploymentStatus(provider, expression);
        }
    }
}