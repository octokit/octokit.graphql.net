namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A GitHub App.
    /// </summary>
    public class App : QueryableValue<App>
    {
        internal App(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        /// <summary>
        /// The description of the app.
        /// </summary>
        public string Description { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public ID Id { get; }

        /// <summary>
        /// The hex color code, without the leading '#', for the logo background.
        /// </summary>
        public string LogoBackgroundColor { get; }

        /// <summary>
        /// A URL pointing to the app's logo.
        /// </summary>
        /// <param name="size">The size of the resulting image.</param>
        public string LogoUrl(Arg<int>? size = null) => default;

        /// <summary>
        /// The name of the app.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A slug based on the name of the app for use in URLs.
        /// </summary>
        public string Slug { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The URL to the app's homepage.
        /// </summary>
        public string Url { get; }

        internal static App Create(Expression expression)
        {
            return new App(expression);
        }
    }
}