using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepositoryOrderField
    {
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,

        [EnumMember(Value = "UPDATED_AT")]
        UpdatedAt,

        [EnumMember(Value = "PUSHED_AT")]
        PushedAt,

        [EnumMember(Value = "NAME")]
        Name,

        [EnumMember(Value = "STARGAZERS")]
        Stargazers,
    }
}