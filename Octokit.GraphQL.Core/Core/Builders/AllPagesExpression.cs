using System;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core.Builders
{
    /// <summary>
    /// Marker to denote that an AllPages() call was made on a method.
    /// </summary>
    class AllPagesExpression : Expression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AllPagesExpression"/> class.
        /// </summary>
        /// <param name="method">The method that AllPages() was called on.</param>
        public AllPagesExpression(MethodCallExpression method) => Method = method;

        /// <summary>
        /// Gets the method that AllPages() was called on.
        /// </summary>
        public MethodCallExpression Method { get; }

        /// <inheritdoc/>
        public override ExpressionType NodeType => ExpressionType.Extension;

        /// <inheritdoc/>
        public override Type Type => Method.Type;
    }
}
