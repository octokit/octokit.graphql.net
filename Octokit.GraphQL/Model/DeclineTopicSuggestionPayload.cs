namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of DeclineTopicSuggestion
    /// </summary>
    public class DeclineTopicSuggestionPayload : QueryableValue<DeclineTopicSuggestionPayload>
    {
        internal DeclineTopicSuggestionPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The declined topic.
        /// </summary>
        [Obsolete(@"Suggested topics are no longer supported Removal on 2024-04-01 UTC.")]
        public Topic Topic => this.CreateProperty(x => x.Topic, Octokit.GraphQL.Model.Topic.Create);

        internal static DeclineTopicSuggestionPayload Create(Expression expression)
        {
            return new DeclineTopicSuggestionPayload(expression);
        }
    }
}