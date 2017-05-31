namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an 'unlabeled' event on a given issue or pull request.
    /// </summary>
    public class UnlabeledEvent : QueryEntity
    {
        public UnlabeledEvent(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the 'unlabel' event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        public string Id { get; }

        /// <summary>
        /// Identifies the label associated with the 'unlabeled' event.
        /// </summary>
        public Label Label => this.CreateProperty(x => x.Label, Octokit.GraphQL.Label.Create);

        /// <summary>
        /// Identifies the `Labelable` associated with the event.
        /// </summary>
        public ILabelable Labelable => this.CreateProperty(x => x.Labelable, Octokit.GraphQL.Internal.StubILabelable.Create);

        internal static UnlabeledEvent Create(IQueryProvider provider, Expression expression)
        {
            return new UnlabeledEvent(provider, expression);
        }
    }
}