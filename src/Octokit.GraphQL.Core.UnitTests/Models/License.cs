using System;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class License : QueryableValue<License>
    {
        public License(Expression expression) : base(expression)
        {
        }

        public string Body { get; }
        public IQueryableList<LicenseRule> Conditions => this.CreateProperty(x => x.Conditions);
        public string Description { get; }

        internal static License Create(Expression expression)
        {
            return new License(expression);
        }
    }
}