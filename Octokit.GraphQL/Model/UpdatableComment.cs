namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Comments that can be updated.
    /// </summary>
    public interface IUpdatableComment : IQueryableValue<IUpdatableComment>
    {
        /// <summary>
        /// Reasons why the current viewer can not update this comment.
        /// </summary>
        IEnumerable<CommentCannotUpdateReason> ViewerCannotUpdateReasons { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIUpdatableComment : QueryableValue<StubIUpdatableComment>, IUpdatableComment
    {
        public StubIUpdatableComment(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public IEnumerable<CommentCannotUpdateReason> ViewerCannotUpdateReasons { get; }

        internal static StubIUpdatableComment Create(IQueryProvider provider, Expression expression)
        {
            return new StubIUpdatableComment(provider, expression);
        }
    }
}