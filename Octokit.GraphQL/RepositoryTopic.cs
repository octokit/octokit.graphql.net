namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A repository-topic connects a repository to a topic.
    /// </summary>
    public class RepositoryTopic : QueryEntity
    {
        public RepositoryTopic(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string Id { get; }

        /// <summary>
        /// The HTTP path for this repository-topic.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The topic.
        /// </summary>
        public Topic Topic => this.CreateProperty(x => x.Topic, Octokit.GraphQL.Topic.Create);

        /// <summary>
        /// The HTTP url for this repository-topic.
        /// </summary>
        public string Url { get; }

        internal static RepositoryTopic Create(IQueryProvider provider, Expression expression)
        {
            return new RepositoryTopic(provider, expression);
        }
    }
}