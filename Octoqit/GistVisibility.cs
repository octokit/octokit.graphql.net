using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octoqit
{
    /// <summary>
    /// The possible Gist visibility types
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GistVisibility
    {
        /// <summary>
        /// Gists that are public
        /// </summary>
        [EnumMember(Value = "PUBLIC")]
        Public,

        /// <summary>
        /// Gists that are secret
        /// </summary>
        [EnumMember(Value = "SECRET")]
        Secret,

        /// <summary>
        /// Gists that are public and secret
        /// </summary>
        [EnumMember(Value = "ALL")]
        All,
    }
}