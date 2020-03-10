using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible roles for enterprise membership.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnterpriseUserAccountMembershipRole
    {
        /// <summary>
        /// The user is a member of the enterprise membership.
        /// </summary>
        [EnumMember(Value = "MEMBER")]
        Member,

        /// <summary>
        /// The user is an owner of the enterprise membership.
        /// </summary>
        [EnumMember(Value = "OWNER")]
        Owner,
    }
}