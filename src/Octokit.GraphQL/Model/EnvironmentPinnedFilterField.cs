using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which environments connections can be ordered
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnvironmentPinnedFilterField
    {
        /// <summary>
        /// All environments will be returned.
        /// </summary>
        [EnumMember(Value = "ALL")]
        All,

        /// <summary>
        /// Only pinned environment will be returned
        /// </summary>
        [EnumMember(Value = "ONLY")]
        Only,

        /// <summary>
        /// Environments exclude pinned will be returned
        /// </summary>
        [EnumMember(Value = "NONE")]
        None,
    }
}