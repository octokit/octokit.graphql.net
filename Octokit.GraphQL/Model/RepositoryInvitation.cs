namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An invitation for a user to be added to a repository.
    /// </summary>
    public class RepositoryInvitation : QueryEntity
    {
        public RepositoryInvitation(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string Id { get; }

        /// <summary>
        /// The user who received the invitation.
        /// </summary>
        public User Invitee => this.CreateProperty(x => x.Invitee, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The user who created the invitation.
        /// </summary>
        public User Inviter => this.CreateProperty(x => x.Inviter, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The Repository the user is invited to.
        /// </summary>
        public RepositoryInvitationRepository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.RepositoryInvitationRepository.Create);

        internal static RepositoryInvitation Create(IQueryProvider provider, Expression expression)
        {
            return new RepositoryInvitation(provider, expression);
        }
    }
}