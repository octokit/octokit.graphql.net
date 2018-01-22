namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a user who is a member of a team.
    /// </summary>
    public class TeamMemberEdge : QueryableValue<TeamMemberEdge>
    {
        public TeamMemberEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string Cursor { get; }

        /// <summary>
        /// The HTTP path to the organization's member access page.
        /// </summary>
        public string MemberAccessResourcePath { get; }

        /// <summary>
        /// The HTTP URL to the organization's member access page.
        /// </summary>
        public string MemberAccessUrl { get; }

        public User Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The role the member has on the team.
        /// </summary>
        public TeamMemberRole Role { get; }

        internal static TeamMemberEdge Create(IQueryProvider provider, Expression expression)
        {
            return new TeamMemberEdge(provider, expression);
        }
    }
}