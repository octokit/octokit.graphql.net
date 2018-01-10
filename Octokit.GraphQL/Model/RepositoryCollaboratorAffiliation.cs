using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The affiliation type between collaborator and repository.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepositoryCollaboratorAffiliation
    {
        /// <summary>
        /// All collaborators of the repository.
        /// </summary>
        [EnumMember(Value = "ALL")]
        All,

        /// <summary>
        /// All outside collaborators of an organization-owned repository.
        /// </summary>
        [EnumMember(Value = "OUTSIDE")]
        Outside,
    }
}