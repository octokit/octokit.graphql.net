using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which repository invitation connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepositoryInvitationOrderField
    {
        /// <summary>
        /// Order repository invitations by creation time
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,

        /// <summary>
        /// Order repository invitations by invitee login
        /// </summary>
        [Obsolete(@"`INVITEE_LOGIN` is no longer a valid field value. Repository invitations can now be associated with an email, not only an invitee. Removal on 2020-10-01 UTC.")]
        [EnumMember(Value = "INVITEE_LOGIN")]
        InviteeLogin,
    }
}