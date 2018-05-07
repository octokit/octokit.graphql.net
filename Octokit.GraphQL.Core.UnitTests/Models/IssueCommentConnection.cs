using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class IssueCommentConnection : QueryableValue<IssueCommentConnection>
    {
        public IssueCommentConnection(Expression expression) : base(expression)
        {
        }

        public IQueryableList<IssueComment> Nodes => this.CreateProperty(x => x.Nodes);
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Models.PageInfo.Create);
        public int TotalCount { get; }

        internal static IssueCommentConnection Create(Expression expression)
        {
            return new IssueCommentConnection(expression);
        }
    }
}