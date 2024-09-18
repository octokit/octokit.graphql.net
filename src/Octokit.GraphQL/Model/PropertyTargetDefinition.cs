namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A property that must match
    /// </summary>
    public class PropertyTargetDefinition : QueryableValue<PropertyTargetDefinition>
    {
        internal PropertyTargetDefinition(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The name of the property
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The values to match for
        /// </summary>
        public IEnumerable<string> PropertyValues { get; }

        /// <summary>
        /// The source of the property. Choose 'custom' or 'system'. Defaults to 'custom' if not specified
        /// </summary>
        public string Source { get; }

        internal static PropertyTargetDefinition Create(Expression expression)
        {
            return new PropertyTargetDefinition(expression);
        }
    }
}