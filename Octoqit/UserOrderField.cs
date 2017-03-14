using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octoqit
{
    /// <summary>
    /// Properties by which user connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UserOrderField
    {
        /// <summary>
        /// Allows ordering a list of users by their login.
        /// </summary>
        [EnumMember(Value = "LOGIN")]
        Login,

        /// <summary>
        /// Allows ordering a list of users by their ability action
        /// </summary>
        [EnumMember(Value = "ACTION")]
        Action,
    }
}