namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a given language found in repositories.
    /// </summary>
    public class Language : QueryableValue<Language>
    {
        public Language(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// The color defined for the current language.
        /// </summary>
        public string Color { get; }

        public string Id { get; }

        /// <summary>
        /// The name of the current language.
        /// </summary>
        public string Name { get; }

        internal static Language Create(IQueryProvider provider, Expression expression)
        {
            return new Language(provider, expression);
        }
    }
}