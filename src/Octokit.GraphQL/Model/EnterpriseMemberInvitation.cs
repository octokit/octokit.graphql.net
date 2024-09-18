namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An invitation for a user to become an unaffiliated member of an enterprise.
    /// </summary>
    public class EnterpriseMemberInvitation : QueryableValue<EnterpriseMemberInvitation>
    {
        internal EnterpriseMemberInvitation(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The email of the person who was invited to the enterprise.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// The enterprise the invitation is for.
        /// </summary>
        public Enterprise Enterprise => this.CreateProperty(x => x.Enterprise, Octokit.GraphQL.Model.Enterprise.Create);

        /// <summary>
        /// The Node ID of the EnterpriseMemberInvitation object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The user who was invited to the enterprise.
        /// </summary>
        public User Invitee => this.CreateProperty(x => x.Invitee, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The user who created the invitation.
        /// </summary>
        public User Inviter => this.CreateProperty(x => x.Inviter, Octokit.GraphQL.Model.User.Create);

        internal static EnterpriseMemberInvitation Create(Expression expression)
        {
            return new EnterpriseMemberInvitation(expression);
        }
    }
}