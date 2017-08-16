namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a mention made by one issue or pull request to another.
    /// </summary>
    public class CrossReferencedEvent : QueryEntity
    {
        public CrossReferencedEvent(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        public string Id { get; }

        /// <summary>
        /// Reference originated in a different repository.
        /// </summary>
        public bool IsCrossRepository { get; }

        /// <summary>
        /// Identifies when the reference was made.
        /// </summary>
        public string ReferencedAt { get; }

        /// <summary>
        /// The HTTP path for this pull request.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Issue or pull request that made the reference.
        /// </summary>
        public ReferencedSubject Source => this.CreateProperty(x => x.Source, Octokit.GraphQL.ReferencedSubject.Create);

        /// <summary>
        /// Issue or pull request to which the reference was made.
        /// </summary>
        public ReferencedSubject Target => this.CreateProperty(x => x.Target, Octokit.GraphQL.ReferencedSubject.Create);

        /// <summary>
        /// The HTTP URL for this pull request.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Checks if the target will be closed when the source is merged.
        /// </summary>
        public bool WillCloseTarget { get; }

        internal static CrossReferencedEvent Create(IQueryProvider provider, Expression expression)
        {
            return new CrossReferencedEvent(provider, expression);
        }
    }
}