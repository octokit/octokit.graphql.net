using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class IssueConnection : QueryableValue<IssueConnection>
    {
        public IssueConnection(Expression expression) : base(expression)
        {
        }

        public IQueryableList<IssueEdge> Edges => this.CreateProperty(x => x.Edges);
        public IQueryableList<Issue> Nodes => this.CreateProperty(x => x.Nodes);
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Models.PageInfo.Create);
        public int TotalCount { get; }

        internal static IssueConnection Create(Expression expression)
        {
            return new IssueConnection(expression);
        }
    }
}