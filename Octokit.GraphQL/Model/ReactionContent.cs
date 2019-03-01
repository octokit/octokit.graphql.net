using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Emojis that can be attached to Issues, Pull Requests and Comments.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ReactionContent
    {
        /// <summary>
        /// Represents the 👍 emoji.
        /// </summary>
        [EnumMember(Value = "THUMBS_UP")]
        ThumbsUp,

        /// <summary>
        /// Represents the 👎 emoji.
        /// </summary>
        [EnumMember(Value = "THUMBS_DOWN")]
        ThumbsDown,

        /// <summary>
        /// Represents the 😄 emoji.
        /// </summary>
        [EnumMember(Value = "LAUGH")]
        Laugh,

        /// <summary>
        /// Represents the 🎉 emoji.
        /// </summary>
        [EnumMember(Value = "HOORAY")]
        Hooray,

        /// <summary>
        /// Represents the 😕 emoji.
        /// </summary>
        [EnumMember(Value = "CONFUSED")]
        Confused,

        /// <summary>
        /// Represents the ❤️ emoji.
        /// </summary>
        [EnumMember(Value = "HEART")]
        Heart,

        /// <summary>
        /// Represents the 🚀 emoji.
        /// </summary>
        [EnumMember(Value = "ROCKET")]
        Rocket,

        /// <summary>
        /// Represents the 👀 emoji.
        /// </summary>
        [EnumMember(Value = "EYES")]
        Eyes,
    }
}