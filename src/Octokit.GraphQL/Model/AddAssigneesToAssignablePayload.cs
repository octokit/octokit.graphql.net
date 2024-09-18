namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of AddAssigneesToAssignable.
    /// </summary>
    public class AddAssigneesToAssignablePayload : QueryableValue<AddAssigneesToAssignablePayload>
    {
        internal AddAssigneesToAssignablePayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The item that was assigned.
        /// </summary>
        public IAssignable Assignable => this.CreateProperty(x => x.Assignable, Octokit.GraphQL.Model.Internal.StubIAssignable.Create);

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        internal static AddAssigneesToAssignablePayload Create(Expression expression)
        {
            return new AddAssigneesToAssignablePayload(expression);
        }
    }
}