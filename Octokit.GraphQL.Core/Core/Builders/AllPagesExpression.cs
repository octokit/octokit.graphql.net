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
        /// <param name="constant">The ConstantExpression that AllPages was sent</param>
        public AllPagesExpression(MethodCallExpression method, ConstantExpression constant = null)
        {
            Method = method;
            Constant = constant;
        }

        /// <summary>
        /// Gets the method that AllPages() was called on.
        /// </summary>
        public MethodCallExpression Method { get; }

        /// <summary>
        /// Gets the constant value that was sent to AllPages
        /// </summary>
        public ConstantExpression Constant { get; }

        /// <inheritdoc/>
        public override ExpressionType NodeType => ExpressionType.Extension;

        /// <inheritdoc/>
        public override Type Type => Method.Type;
    }
}
