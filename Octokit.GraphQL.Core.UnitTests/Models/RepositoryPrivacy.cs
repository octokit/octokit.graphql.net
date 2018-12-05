using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepositoryPrivacy
    {
        [EnumMember(Value = "PUBLIC")]
        Public,

        [EnumMember(Value = "PRIVATE")]
        Private,
    }
}