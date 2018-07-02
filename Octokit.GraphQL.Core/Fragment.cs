using System;
using System.Linq.Expressions;

namespace Octokit.GraphQL
{
    /// <summary>
    /// Fragment Interface
    /// </summary>
    public interface IFragment
    {
        /// <summary>
        /// The name of a fragment
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The expression to retrieve the value from the result
        /// </summary>
        Expression Expression { get; }

        /// <summary>
        /// Input type for the fragment
        /// </summary>
        Type InputType { get; }

        /// <summary>
        /// The output type of the fragment
        /// </summary>
        Type ReturnType { get; }
    }

    /// <summary>
    /// Fragment
    /// </summary>
    /// <typeparam name="TInput">Input type for the fragment</typeparam>
    /// <typeparam name="TOutput">The output type of the fragment</typeparam>
    public class Fragment<TInput, TOutput>: IFragment
    {
        /// <inheritdoc />
        public string Name { get; }

        /// <summary>
        /// The expression to retrieve the value from the result
        /// </summary>
        public Expression<Func<TInput, TOutput>> Expression { get; }

        /// <inheritdoc />
        Type IFragment.InputType => typeof(TInput);
        /// <inheritdoc />
        Type IFragment.ReturnType => typeof(TOutput);

        /// <inheritdoc />
        Expression IFragment.Expression => Expression;

        /// <summary>
        /// Create a Fragment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="expression"></param>
        public Fragment(string name, Expression<Func<TInput, TOutput>> expression)
        {
            Name = name;
            Expression = expression;
        }
    }
}
