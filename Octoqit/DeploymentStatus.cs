namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Describes the status of a given deployment attempt.
    /// </summary>
    public class DeploymentStatus : QueryEntity
    {
        public DeploymentStatus(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the user who triggered the deployment.
        /// </summary>
        public User Creator => this.CreateProperty(x => x.Creator, Octoqit.User.Create);

        /// <summary>
        /// Identifies the deployment associated with status.
        /// </summary>
        public Deployment Deployment => this.CreateProperty(x => x.Deployment, Octoqit.Deployment.Create);

        /// <summary>
        /// Identifies the description of the deployment.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Identifies the environment url of the deployment.
        /// </summary>
        public string EnvironmentUrl { get; }

        public string Id { get; }

        /// <summary>
        /// Identifies the log url of the deployment.
        /// </summary>
        public string LogUrl { get; }

        /// <summary>
        /// Identifies the current state of the deployment.
        /// </summary>
        public DeploymentState State { get; }

        internal static DeploymentStatus Create(IQueryProvider provider, Expression expression)
        {
            return new DeploymentStatus(provider, expression);
        }
    }
}