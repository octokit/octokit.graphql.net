namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A repository deploy key.
    /// </summary>
    public class DeployKey : QueryEntity
    {
        public DeployKey(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        public string Id { get; }

        /// <summary>
        /// The deploy key.
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Whether or not the deploy key is read only.
        /// </summary>
        public bool ReadOnly { get; }

        /// <summary>
        /// The deploy key title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Whether or not the deploy key has been verified.
        /// </summary>
        public bool Verified { get; }

        internal static DeployKey Create(IQueryProvider provider, Expression expression)
        {
            return new DeployKey(provider, expression);
        }
    }
}