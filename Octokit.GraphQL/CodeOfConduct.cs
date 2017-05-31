namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The Code of Conduct for a repository
    /// </summary>
    public class CodeOfConduct : QueryEntity
    {
        public CodeOfConduct(IQueryProvider provider, Expression expression) : base(provider, expression)
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

        internal static CodeOfConduct Create(IQueryProvider provider, Expression expression)
        {
            return new CodeOfConduct(provider, expression);
        }
    }
}