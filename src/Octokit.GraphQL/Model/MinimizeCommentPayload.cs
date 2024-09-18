namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of MinimizeComment.
    /// </summary>
    public class MinimizeCommentPayload : QueryableValue<MinimizeCommentPayload>
    {
        internal MinimizeCommentPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The comment that was minimized.
        /// </summary>
        public IMinimizable MinimizedComment => this.CreateProperty(x => x.MinimizedComment, Octokit.GraphQL.Model.Internal.StubIMinimizable.Create);

        internal static MinimizeCommentPayload Create(Expression expression)
        {
            return new MinimizeCommentPayload(expression);
        }
    }
}