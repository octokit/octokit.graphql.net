using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which contribution connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ContributionOrderField
    {
        /// <summary>
        /// Order contributions by when they were made.
        /// </summary>
        [EnumMember(Value = "OCCURRED_AT")]
        OccurredAt,
    }
}