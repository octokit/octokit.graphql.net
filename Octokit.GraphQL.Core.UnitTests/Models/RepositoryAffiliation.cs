using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepositoryAffiliation
    {
        [EnumMember(Value = "OWNER")]
        Owner,

        [EnumMember(Value = "COLLABORATOR")]
        Collaborator,

        [EnumMember(Value = "ORGANIZATION_MEMBER")]
        OrganizationMember,
    }
}