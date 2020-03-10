using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible capabilities for action executions setting.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ActionExecutionCapabilitySetting
    {
        /// <summary>
        /// All action executions are disabled.
        /// </summary>
        [EnumMember(Value = "DISABLED")]
        Disabled,

        /// <summary>
        /// All action executions are enabled.
        /// </summary>
        [EnumMember(Value = "ALL_ACTIONS")]
        AllActions,

        /// <summary>
        /// Only actions defined within the repo are allowed.
        /// </summary>
        [EnumMember(Value = "LOCAL_ACTIONS_ONLY")]
        LocalActionsOnly,

        /// <summary>
        /// Organization administrators action execution capabilities.
        /// </summary>
        [EnumMember(Value = "NO_POLICY")]
        NoPolicy,
    }
}