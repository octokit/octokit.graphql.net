using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL
{
    /// <summary>
    /// The possible deployment states.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DeploymentState
    {
        /// <summary>
        /// The deployment is pending.
        /// </summary>
        [EnumMember(Value = "PENDING")]
        Pending,

        /// <summary>
        /// The deployment was successful.
        /// </summary>
        [EnumMember(Value = "SUCCESS")]
        Success,

        /// <summary>
        /// The deployment has failed.
        /// </summary>
        [EnumMember(Value = "FAILURE")]
        Failure,

        /// <summary>
        /// The deployment is inactive.
        /// </summary>
        [EnumMember(Value = "INACTIVE")]
        Inactive,

        /// <summary>
        /// The deployment experienced an error.
        /// </summary>
        [EnumMember(Value = "ERROR")]
        Error,
    }
}