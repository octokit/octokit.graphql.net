namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The Code of Conduct for a repository
    /// </summary>
    public class CodeOfConduct : QueryableValue<CodeOfConduct>
    {
        internal CodeOfConduct(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The body of the CoC
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The key for the CoC
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// The formal name of the CoC
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The path to the CoC
        /// </summary>
        public string Url { get; }

        internal static CodeOfConduct Create(Expression expression)
        {
            return new CodeOfConduct(expression);
        }
    }
}