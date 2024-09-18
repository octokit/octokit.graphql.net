using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which pinned environments connections can be ordered
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PinnedEnvironmentOrderField
    {
        /// <summary>
        /// Order pinned environments by position
        /// </summary>
        [EnumMember(Value = "POSITION")]
        Position,
    }
}