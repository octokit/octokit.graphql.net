using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL
{
    /// <summary>
    /// The possible errors that will prevent a user from editting a comment.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CommentCannotEditReason
    {
        /// <summary>
        /// You must be the author or have write access to this repository to edit this comment.
        /// </summary>
        [EnumMember(Value = "INSUFFICIENT_ACCESS")]
        InsufficientAccess,

        /// <summary>
        /// Unable to create comment because issue is locked.
        /// </summary>
        [EnumMember(Value = "LOCKED")]
        Locked,

        /// <summary>
        /// You must be logged in to edit this comment.
        /// </summary>
        [EnumMember(Value = "LOGIN_REQUIRED")]
        LoginRequired,

        /// <summary>
        /// Repository is under maintenance.
        /// </summary>
        [EnumMember(Value = "MAINTENANCE")]
        Maintenance,

        /// <summary>
        /// At least one email address must be verified to edit this comment.
        /// </summary>
        [EnumMember(Value = "VERIFIED_EMAIL_REQUIRED")]
        VerifiedEmailRequired,
    }
}