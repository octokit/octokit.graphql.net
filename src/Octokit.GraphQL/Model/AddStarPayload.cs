namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of AddStar.
    /// </summary>
    public class AddStarPayload : QueryableValue<AddStarPayload>
    {
        internal AddStarPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The starrable.
        /// </summary>
        public IStarrable Starrable => this.CreateProperty(x => x.Starrable, Octokit.GraphQL.Model.Internal.StubIStarrable.Create);

        internal static AddStarPayload Create(Expression expression)
        {
            return new AddStarPayload(expression);
        }
    }
}