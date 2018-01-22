namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A topic aggregates entities that are related to a subject.
    /// </summary>
    public class Topic : QueryableValue<Topic>
    {
        public Topic(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string Id { get; }

        /// <summary>
        /// The topic's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A list of related topics, including aliases of this topic, sorted with the most relevant
        /// first.
        /// </summary>
        public IQueryableList<Topic> RelatedTopics => this.CreateProperty(x => x.RelatedTopics);

        internal static Topic Create(IQueryProvider provider, Expression expression)
        {
            return new Topic(provider, expression);
        }
    }
}