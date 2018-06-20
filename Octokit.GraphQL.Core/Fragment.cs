using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Octokit.GraphQL
{
    public interface IFragment
    {
        string Name { get; }
        Expression Expression { get; }
        Type InputType { get; }
        Type ReturnType { get; }
    }

    public class Fragment<TInput, TOutput>: IFragment
    {
        public string Name { get; }
        public Expression<Func<TInput, TOutput>> Expression { get; }

        Type IFragment.InputType => typeof(TInput);
        Type IFragment.ReturnType => typeof(TOutput);

        Expression IFragment.Expression => Expression;

        public Fragment(string name, Expression<Func<TInput, TOutput>> expression)
        {
            Name = name;
            Expression = expression;
        }
    }
}
