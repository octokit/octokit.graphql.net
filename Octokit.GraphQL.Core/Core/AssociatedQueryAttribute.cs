using System;

namespace Octokit.GraphQL.Core
{
    /// <summary>
    /// Attribute applied to mutations to specify the associated root query.
    /// </summary>
    /// <remarks>
    /// In order for auto-paging to work in a mutation, there needs to be way to locate
    /// the associated root query to get access to the `.Node` method.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AssociatedQueryAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssociatedQueryAttribute"/> class.
        /// </summary>
        /// <param name="type">The type of the associated query.</param>
        public AssociatedQueryAttribute(Type type) => Type = type;

        /// <summary>
        /// The type of the associated query.
        /// </summary>
        public Type Type { get; }
    }
}
