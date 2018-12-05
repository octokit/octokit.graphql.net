using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderDirection
    {
        [EnumMember(Value = "ASC")]
        Asc,

        [EnumMember(Value = "DESC")]
        Desc,
    }
}