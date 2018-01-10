using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Octokit.GraphQL.Core.Syntax;
using Octokit.GraphQL.Core.Utilities;
using Newtonsoft.Json.Linq;

namespace Octokit.GraphQL.Core.Builders
{
    public class QueryBuilder : ExpressionVisitor
    {
        static readonly ParameterExpression RootDataParameter = Expression.Parameter(typeof(JToken), "data");

        OperationDefinition root;
        SyntaxTree syntax;
        Dictionary<ParameterExpression, LambdaParameter> lambdaParameters;

        public GraphQLQuery<TResult> Build<TResult>(IQueryable<TResult> query)
        {
            root = null;
            syntax = new SyntaxTree();
            lambdaParameters = new Dictionary<ParameterExpression, LambdaParameter>();

            var rewritten = Visit(query.Expression);
            var expression = Expression.Lambda<Func<JObject, IEnumerable<TResult>>>(
                rewritten.AddCast(typeof(IQueryable<TResult>)),
                RootDataParameter);
            return new GraphQLQuery<TResult>(root, expression);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var left = BookmarkAndVisit(node.Left).AddCast(node.Left.Type);
            var right = BookmarkAndVisit(node.Right).AddCast(node.Right.Type);
            return node.Update(left, node.Conversion, right);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (syntax.Root == null)
            {
                var query = node.Value as IQuery;
                var mutation = node.Value as IMutation;
                var queryEntity = node.Value as IQueryEntity;

                if (query != null)
                {
                    root = syntax.AddRoot(OperationType.Query, null);
                    return RootDataParameter.AddIndexer("data");
                }
                else if (mutation != null)
                {
                    root = syntax.AddRoot(OperationType.Mutation, null);
                    return RootDataParameter.AddIndexer("data");
                }
                else if (queryEntity != null)
                {
                    return Visit(queryEntity.Expression);
                }
            }

            return node;
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            var parameters = RewriteParameters(node.Parameters);
            var body = Visit(node.Body);
            return Expression.Lambda(body, parameters);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            return VisitMember(node, null);
        }

        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            if (IsQueryEntityMember(node.Expression))
            {
                var expression = BookmarkAndVisit(node.Expression).AddCast(node.Expression.Type);
                return Expression.Bind(node.Member, expression);
            }
            else
            {
                return node.Update(BookmarkAndVisit(node.Expression));
            }
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            return VisitMethodCall(node, null);
        }

        protected override Expression VisitNew(NewExpression node)
        {
            var newArguments = new List<Expression>();
            var index = 0;

            foreach (var arg in node.Arguments)
            {
                using (syntax.Bookmark())
                {
                    var alias = node.Members?[index];
                    Expression rewritten;

                    if (arg is MemberExpression member)
                    {
                        rewritten = VisitMember(member, alias);
                    }
                    else if (arg is MethodCallExpression call)
                    {
                        rewritten = VisitMethodCall(call, alias);
                    }
                    else
                    {
                        rewritten = Visit(arg);
                    }

                    newArguments.Add(rewritten.AddCast(arg.Type));
                }

                ++index;
            }

            return node.Members != null ?
                Expression.New(node.Constructor, newArguments, node.Members) :
                Expression.New(node.Constructor, newArguments);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (IsQueryEntity(node.Type))
            {
                LambdaParameter result;

                if (lambdaParameters.TryGetValue(node, out result))
                {
                    return result.Rewritten;
                }
                else
                {
                    throw new Exception(
                        "Internal Error: encountered a lambda parameter that hasn't previously been rewritten.");
                }
            }

            return node;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            return node.Update(Visit(node.Operand));
        }

        private Expression VisitMember(MemberExpression node, MemberInfo alias)
        {
            if (IsUnionMember(node.Member))
            {
                var source = Visit(node.Expression);
                var fragment = syntax.AddInlineFragment(((PropertyInfo)node.Member).PropertyType, true);
                return Expression.Call(
                    ExpressionMethods.ChildrenOfTypeMethod,
                    source,
                    Expression.Constant(fragment.TypeCondition.Name));
            }
            else if (IsQueryEntityMember(node.Member))
            {
                var parameterExpression = node.Expression as ParameterExpression;

                if (parameterExpression != null)
                {
                    var parameter = (ParameterExpression)Visit(parameterExpression);
                    var parentSelection = GetSelectionSet(parameterExpression);
                    var field = syntax.AddField(parentSelection, node.Member, alias);
                    return Visit(parameterExpression).AddIndexer(field);
                }
                else
                {
                    var instance = Visit(node.Expression);
                    var field = syntax.AddField(node.Member, alias);
                    return instance.AddIndexer(field);
                }
            }
            else
            {
                var instance = Visit(node.Expression);

                if (ExpressionWasRewritten(node.Expression, instance))
                {
                    instance = instance.AddCast(node.Expression.Type);
                }

                return node.Update(instance);
            }
        }

        private IEnumerable<Expression> VisitMethodArguments(MethodInfo method, ReadOnlyCollection<Expression> arguments)
        {
            var parameters = method.GetParameters();
            var index = 0;

            foreach (var arg in arguments)
            {
                var parameter = parameters[index++];
                yield return Visit(arg).AddCast(parameter.ParameterType);
            }
        }

        private Expression VisitMethodCall(MethodCallExpression node, MemberInfo alias)
        {
            if (IsSelect(node.Method))
            {
                return VisitSelect(node.Arguments[0], node.Arguments[1]);
            }
            else if (IsOfType(node.Method))
            {
                var source = Visit(node.Arguments[0]);
                var fragment = syntax.AddInlineFragment(node.Method.GetGenericArguments()[0], true);
                return Expression.Call(
                    ExpressionMethods.ChildrenOfTypeMethod,
                    source,
                    Expression.Constant(fragment.TypeCondition.Name));
            }
            else if (IsQueryEntityMember(node.Method))
            {
                return VisitQueryMethod(node, alias);
            }
            else
            {
                try
                {
                    var methodCallExpression = node.Update(Visit(node.Object), VisitMethodArguments(node.Method, node.Arguments));
                    return methodCallExpression;
                }
                catch (NotSupportedException ex)
                {
                    throw new NotSupportedException($"{node.Method.Name}() is not supported", ex);
                }
            }
        }

        private Expression VisitQueryMethod(MethodCallExpression node, MemberInfo alias)
        {
            var queryEntity = (node.Object as ConstantExpression)?.Value as IQueryEntity;
            var instance = Visit(queryEntity?.Expression ?? node.Object);
            var field = syntax.AddField(node.Method, alias);

            VisitQueryMethodArguments(node.Method.GetParameters(), node.Arguments);

            return instance.AddIndexer(field);
        }

        private void VisitQueryMethodArguments(ParameterInfo[] parameters, ReadOnlyCollection<Expression> arguments)
        {
            for (var i = 0; i < parameters.Length; ++i)
            {
                var parameter = parameters[i];
                var constantArgument = arguments[i] as ConstantExpression
                    ?? (arguments[i] as UnaryExpression)?.Operand as ConstantExpression;

                if (constantArgument == null)
                {
                    throw new NotSupportedException("Non-constant field arguments not yet supported.");
                }

                if (constantArgument.Value != null)
                {
                    syntax.AddArgument(parameter.Name, constantArgument.Value);
                }
            }
        }

        private Expression VisitSelect(Expression source, Expression selectExpression)
        {
            var lambda = selectExpression.GetLambda();
            var instance = Visit(source);
            var select = (LambdaExpression)Visit(lambda);
            var isQueryable = GetQueryableResultType(instance.Type) != null;

            if (!isQueryable)
            {
                if (select.ReturnType == typeof(IQueryable<JToken>))
                {
                    return Expression.Invoke(select, instance);
                }
                else
                {
                    return Expression.Call(
                        ExpressionMethods.SelectEntityMethod.MakeGenericMethod(select.ReturnType),
                        instance,
                        select);
                }
            }
            else
            {
                return Expression.Call(
                    ExpressionMethods.SelectMethod.MakeGenericMethod(select.ReturnType),
                    instance,
                    select);
            }
        }

        private IEnumerable<ParameterExpression> RewriteParameters(IEnumerable<ParameterExpression> parameters)
        {
            var result = new List<ParameterExpression>();

            foreach (var parameter in parameters)
            {
                if (IsQueryEntity(parameter.Type))
                {
                    var rewritten = Expression.Parameter(typeof(JToken), parameter.Name);
                    var p = new LambdaParameter(
                        parameter,
                        rewritten,
                        syntax.Head);
                    lambdaParameters.Add(parameter, p);
                    result.Add(rewritten);
                }
                else
                {
                    result.Add(parameter);
                }
            }

            return result;
        }

        private Expression BookmarkAndVisit(Expression left)
        {
            using (syntax.Bookmark())
            {
                return Visit(left);
            }
        }

        private static Type GetQueryableResultType(Type type)
        {
            return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(IQueryable<>) ?
                type.GetTypeInfo().GenericTypeArguments[0] : null;
        }

        private ISelectionSet GetSelectionSet(ParameterExpression parameter)
        {
            return lambdaParameters[parameter].SelectionSet;
        }

        private static bool ExpressionWasRewritten(Expression oldExpression, Expression newExpression)
        {
            return newExpression.Type == typeof(JToken) && oldExpression.Type != typeof(JToken);
        }

        private static bool IsOfType(MethodInfo method)
        {
            return method.DeclaringType == typeof(Queryable) &&
                method.Name == nameof(Queryable.OfType) &&
                method.GetParameters().Length == 1 &&
                method.GetGenericArguments().Length == 1;
        }

        private static bool IsSelect(MethodInfo method)
        {
            return (method.DeclaringType == typeof(Queryable) && method.Name == nameof(Queryable.Select) && method.GetParameters().Length == 2) ||
                   (method.DeclaringType == typeof(QueryEntityExtensions) && method.Name == nameof(QueryEntityExtensions.Select) && method.GetParameters().Length == 2);
        }

        private static bool IsQueryEntity(Type type)
        {
            return typeof(IQueryEntity).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }

        private static bool IsQueryEntityMember(Expression expression)
        {
            var memberExpression = expression as MemberExpression;
            return memberExpression != null && IsQueryEntityMember(memberExpression.Member);
        }

        private static bool IsQueryEntityMember(MemberInfo member)
        {
            return IsQueryEntity(member.DeclaringType);
        }

        private static bool IsUnion(Type type)
        {
            return typeof(IUnion).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }

        private static bool IsUnionMember(MemberInfo member)
        {
            return member is PropertyInfo && IsUnion(member.DeclaringType);
        }

        private class LambdaParameter
        {
            public LambdaParameter(
                ParameterExpression original,
                ParameterExpression rewritten,
                ISelectionSet selectionSet)
            {
                Original = original;
                Rewritten = rewritten;
                SelectionSet = selectionSet;
            }

            public ParameterExpression Original { get; }
            public ParameterExpression Rewritten { get; }
            public ISelectionSet SelectionSet { get; }
        }
    }
}
