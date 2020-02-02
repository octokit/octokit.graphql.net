namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class RegistryPackageEdge : QueryableValue<RegistryPackageEdge>
    {
        internal RegistryPackageEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public RegistryPackage Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.RegistryPackage.Create);

        internal static RegistryPackageEdge Create(Expression expression)
        {
            return new RegistryPackageEdge(expression);
        }
    }
}