using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Method to use when merging changes from queued pull requests.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MergeQueueMergeMethod
    {
        /// <summary>
        /// Merge commit
        /// </summary>
        [EnumMember(Value = "MERGE")]
        Merge,

        /// <summary>
        /// Squash and merge
        /// </summary>
        [EnumMember(Value = "SQUASH")]
        Squash,

        /// <summary>
        /// Rebase and merge
        /// </summary>
        [EnumMember(Value = "REBASE")]
        Rebase,
    }
}