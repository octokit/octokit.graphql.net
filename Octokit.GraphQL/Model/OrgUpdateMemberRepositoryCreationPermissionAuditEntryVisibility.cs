using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The permissions available for repository creation on an Organization.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrgUpdateMemberRepositoryCreationPermissionAuditEntryVisibility
    {
        /// <summary>
        /// All organization members are restricted from creating any repositories.
        /// </summary>
        [EnumMember(Value = "ALL")]
        All,

        /// <summary>
        /// All organization members are restricted from creating public repositories.
        /// </summary>
        [EnumMember(Value = "PUBLIC")]
        Public,
    }
}