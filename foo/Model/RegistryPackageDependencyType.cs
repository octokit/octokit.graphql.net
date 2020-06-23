using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible types of a registry package dependency.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RegistryPackageDependencyType
    {
        /// <summary>
        /// A default registry package dependency type.
        /// </summary>
        [EnumMember(Value = "DEFAULT")]
        Default,

        /// <summary>
        /// A dev registry package dependency type.
        /// </summary>
        [EnumMember(Value = "DEV")]
        Dev,

        /// <summary>
        /// A test registry package dependency type.
        /// </summary>
        [EnumMember(Value = "TEST")]
        Test,

        /// <summary>
        /// A peer registry package dependency type.
        /// </summary>
        [EnumMember(Value = "PEER")]
        Peer,

        /// <summary>
        /// An optional registry package dependency type.
        /// </summary>
        [EnumMember(Value = "OPTIONAL")]
        Optional,

        /// <summary>
        /// An optional registry package dependency type.
        /// </summary>
        [EnumMember(Value = "BUNDLED")]
        Bundled,
    }
}