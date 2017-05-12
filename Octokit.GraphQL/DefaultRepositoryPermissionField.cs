using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL
{
    /// <summary>
    /// The possible default permissions for organization-owned repositories.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DefaultRepositoryPermissionField
    {
        /// <summary>
        /// Members have read access to org repos by default
        /// </summary>
        [EnumMember(Value = "READ")]
        Read,

        /// <summary>
        /// Members have read and write access to org repos by default
        /// </summary>
        [EnumMember(Value = "WRITE")]
        Write,

        /// <summary>
        /// Members have read, write, and admin access to org repos by default
        /// </summary>
        [EnumMember(Value = "ADMIN")]
        Admin,
    }
}