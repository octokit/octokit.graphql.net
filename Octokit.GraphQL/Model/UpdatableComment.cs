namespace Octokit.GraphQL.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Comments that can be updated.
    /// </summary>
    public interface IUpdatableComment : IQueryEntity
    {
        /// <summary>
        /// Reasons why the current viewer can not update this comment.
        /// </summary>
        IQueryable<CommentCannotUpdateReason> ViewerCannotUpdateReasons { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIUpdatableComment : QueryEntity, IUpdatableComment
    {
        public StubIUpdatableComment(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public IQueryable<CommentCannotUpdateReason> ViewerCannotUpdateReasons => this.CreateProperty(x => x.ViewerCannotUpdateReasons);

        internal static StubIUpdatableComment Create(IQueryProvider provider, Expression expression)
        {
            return new StubIUpdatableComment(provider, expression);
        }
    }
}