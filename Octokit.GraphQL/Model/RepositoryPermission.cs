using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The access level to a repository
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepositoryPermission
    {
        /// <summary>
        /// Can read, clone, push, and add collaborators
        /// </summary>
        [EnumMember(Value = "ADMIN")]
        Admin,

        /// <summary>
        /// Can read, clone and push
        /// </summary>
        [EnumMember(Value = "WRITE")]
        Write,

        /// <summary>
        /// Can read and clone
        /// </summary>
        [EnumMember(Value = "READ")]
        Read,
    }
}