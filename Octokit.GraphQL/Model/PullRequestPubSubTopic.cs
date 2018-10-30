using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible PubSub channels for a pull request.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PullRequestPubSubTopic
    {
        /// <summary>
        /// The channel ID for observing pull request updates.
        /// </summary>
        [EnumMember(Value = "UPDATED")]
        Updated,

        /// <summary>
        /// The channel ID for marking an pull request as read.
        /// </summary>
        [EnumMember(Value = "MARKASREAD")]
        Markasread,

        /// <summary>
        /// The channel ID for observing head ref updates.
        /// </summary>
        [EnumMember(Value = "HEAD_REF")]
        HeadRef,

        /// <summary>
        /// The channel ID for updating items on the pull request timeline.
        /// </summary>
        [EnumMember(Value = "TIMELINE")]
        Timeline,

        /// <summary>
        /// The channel ID for observing pull request state updates.
        /// </summary>
        [EnumMember(Value = "STATE")]
        State,
    }
}