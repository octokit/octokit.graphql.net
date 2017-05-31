namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents items that can be performed via integrations.
    /// </summary>
    public interface IPerformableViaIntegration : IQueryEntity
    {
        /// <summary>
        /// The integration that created this object.
        /// </summary>
        Integration ViaIntegration { get; }
    }
}

namespace Octokit.GraphQL.Internal
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIPerformableViaIntegration : QueryEntity, IPerformableViaIntegration
    {
        public StubIPerformableViaIntegration(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public Integration ViaIntegration => this.CreateProperty(x => x.ViaIntegration, Octokit.GraphQL.Integration.Create);

        internal static StubIPerformableViaIntegration Create(IQueryProvider provider, Expression expression)
        {
            return new StubIPerformableViaIntegration(provider, expression);
        }
    }
}