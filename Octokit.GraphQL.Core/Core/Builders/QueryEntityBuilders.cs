using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Octokit.GraphQL.Core.Builders
{
    public static class QueryEntityBuilders
    {
        public static IQueryableList<TValue> CreateMethodCall<TObject, TValue>(
            this TObject o,
            Expression<Func<TObject, IQueryableList<TValue>>> selector)
                where TObject : IQueryableValue
        {
            var methodCall = (MethodCallExpression)selector.Body;
            return new QueryableList<TValue>(
                methodCall.Update(Expression.Constant(o), methodCall.Arguments));
        }

        public static TValue CreateMethodCall<TObject, TValue>(
            this TObject o,
            Expression<Func<TObject, TValue>> selector,
            Func<Expression, TValue> create)
                where TObject : IQueryableValue
                where TValue : IQueryableValue
        {
            var methodCall = (MethodCallExpression)selector.Body;
            return create(methodCall.Update(Expression.Constant(o), methodCall.Arguments));
        }

        public static IPagedList<TValue> CreatePagedMethodCall<TObject, TValue>(
            this TObject o,
            Expression<Func<TObject, TValue>> selector)
                where TObject : IQueryableValue
                where TValue : IPagingConnection
        {
            var methodCall = (MethodCallExpression)selector.Body;
            return new PagedList<TValue>(
                methodCall.Update(Expression.Constant(o), methodCall.Arguments),
                selector);
        }

        public static TValue CreateProperty<TObject, TValue>(
            this TObject o,
            Expression<Func<TObject, TValue>> selector,
            Func<Expression, TValue> create)
                where TObject : IQueryableValue
        {
            var memberExpression = (MemberExpression)selector.Body;
            return create(memberExpression.Update(Expression.Constant(o)));
        }

        public static IQueryableList<TValue> CreateProperty<TObject, TValue>(
            this TObject o,
            Expression<Func<TObject, IQueryableList<TValue>>> selector)
                where TObject : IQueryableValue
                where TValue : IQueryableValue
        {
            var memberExpression = (MemberExpression)selector.Body;
            return new QueryableList<TValue>(
                memberExpression.Update(Expression.Constant(o)));
        }
    }
}
