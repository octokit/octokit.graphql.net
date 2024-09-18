using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible team review assignment algorithms
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TeamReviewAssignmentAlgorithm
    {
        /// <summary>
        /// Alternate reviews between each team member
        /// </summary>
        [EnumMember(Value = "ROUND_ROBIN")]
        RoundRobin,

        /// <summary>
        /// Balance review load across the entire team
        /// </summary>
        [EnumMember(Value = "LOAD_BALANCE")]
        LoadBalance,
    }
}