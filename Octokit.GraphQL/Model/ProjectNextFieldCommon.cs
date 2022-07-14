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
    /// Common fields across different field types
    /// </summary>
    public interface IProjectNextFieldCommon : IQueryableValue<IProjectNextFieldCommon>, IQueryableInterface
    {
        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The field's type.
        /// </summary>
        ProjectNextFieldType DataType { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        int? DatabaseId { get; }

        ID Id { get; }

        /// <summary>
        /// The project field's name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The project that contains this field.
        /// </summary>
        ProjectNext Project { get; }

        /// <summary>
        /// The field's settings.
        /// </summary>
        string Settings { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        DateTimeOffset UpdatedAt { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIProjectNextFieldCommon : QueryableValue<StubIProjectNextFieldCommon>, IProjectNextFieldCommon
    {
        internal StubIProjectNextFieldCommon(Expression expression) : base(expression)
        {
        }

        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public DateTimeOffset CreatedAt { get; }

        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public ProjectNextFieldType DataType { get; }

        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public int? DatabaseId { get; }

        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public ID Id { get; }

        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public string Name { get; }

        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public ProjectNext Project => this.CreateProperty(x => x.Project, Octokit.GraphQL.Model.ProjectNext.Create);

        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public string Settings { get; }

        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public DateTimeOffset UpdatedAt { get; }

        internal static StubIProjectNextFieldCommon Create(Expression expression)
        {
            return new StubIProjectNextFieldCommon(expression);
        }
    }
}