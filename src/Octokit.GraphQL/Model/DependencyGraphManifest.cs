namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Dependency manifest for a repository
    /// </summary>
    public class DependencyGraphManifest : QueryableValue<DependencyGraphManifest>
    {
        internal DependencyGraphManifest(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Path to view the manifest file blob
        /// </summary>
        public string BlobPath { get; }

        /// <summary>
        /// A list of manifest dependencies
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public DependencyGraphDependencyConnection Dependencies(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Dependencies(first, after, last, before), Octokit.GraphQL.Model.DependencyGraphDependencyConnection.Create);

        /// <summary>
        /// The number of dependencies listed in the manifest
        /// </summary>
        public int? DependenciesCount { get; }

        /// <summary>
        /// Is the manifest too big to parse?
        /// </summary>
        public bool ExceedsMaxSize { get; }

        /// <summary>
        /// Fully qualified manifest filename
        /// </summary>
        public string Filename { get; }

        /// <summary>
        /// The Node ID of the DependencyGraphManifest object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Were we able to parse the manifest?
        /// </summary>
        public bool Parseable { get; }

        /// <summary>
        /// The repository containing the manifest
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        internal static DependencyGraphManifest Create(Expression expression)
        {
            return new DependencyGraphManifest(expression);
        }
    }
}