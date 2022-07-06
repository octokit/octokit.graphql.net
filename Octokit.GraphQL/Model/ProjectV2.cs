namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// New projects that manage issues, pull requests and drafts using tables and boards.
    /// </summary>
    public class ProjectV2 : QueryableValue<ProjectV2>
    {
        internal ProjectV2(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Returns true if the project is closed.
        /// </summary>
        public bool Closed { get; }

        /// <summary>
        /// Identifies the date and time when the object was closed.
        /// </summary>
        public DateTimeOffset? ClosedAt { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The actor who originally created the project.
        /// </summary>
        public IActor Creator => this.CreateProperty(x => x.Creator, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        /// <summary>
        /// A field of the project
        /// </summary>
        /// <param name="name">The name of the field</param>
        public ProjectV2FieldConfiguration Field(Arg<string> name) => this.CreateMethodCall(x => x.Field(name), Octokit.GraphQL.Model.ProjectV2FieldConfiguration.Create);

        /// <summary>
        /// List of fields and their constraints in the project
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for project v2 fields returned from the connection</param>
        public ProjectV2FieldConfigurationConnection Fields(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ProjectV2FieldOrder>? orderBy = null) => this.CreateMethodCall(x => x.Fields(first, after, last, before, orderBy), Octokit.GraphQL.Model.ProjectV2FieldConfigurationConnection.Create);

        public ID Id { get; }

        /// <summary>
        /// List of items in the project
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for project v2 items returned from the connection</param>
        public ProjectV2ItemConnection Items(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ProjectV2ItemOrder>? orderBy = null) => this.CreateMethodCall(x => x.Items(first, after, last, before, orderBy), Octokit.GraphQL.Model.ProjectV2ItemConnection.Create);

        /// <summary>
        /// The project's number.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// The project's owner. Currently limited to organizations and users.
        /// </summary>
        public IProjectV2Owner Owner => this.CreateProperty(x => x.Owner, Octokit.GraphQL.Model.Internal.StubIProjectV2Owner.Create);

        /// <summary>
        /// Returns true if the project is public.
        /// </summary>
        public bool Public { get; }

        /// <summary>
        /// The project's readme.
        /// </summary>
        public string Readme { get; }

        /// <summary>
        /// The repositories the project is linked to.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for repositories returned from the connection</param>
        public RepositoryConnection Repositories(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<RepositoryOrder>? orderBy = null) => this.CreateMethodCall(x => x.Repositories(first, after, last, before, orderBy), Octokit.GraphQL.Model.RepositoryConnection.Create);

        /// <summary>
        /// The HTTP path for this project
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The project's short description.
        /// </summary>
        public string ShortDescription { get; }

        /// <summary>
        /// The project's name.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this project
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Check if the current viewer can update this object.
        /// </summary>
        public bool ViewerCanUpdate { get; }

        /// <summary>
        /// List of views in the project
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for project v2 views returned from the connection</param>
        public ProjectV2ViewConnection Views(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ProjectV2ViewOrder>? orderBy = null) => this.CreateMethodCall(x => x.Views(first, after, last, before, orderBy), Octokit.GraphQL.Model.ProjectV2ViewConnection.Create);

        internal static ProjectV2 Create(Expression expression)
        {
            return new ProjectV2(expression);
        }
    }
}