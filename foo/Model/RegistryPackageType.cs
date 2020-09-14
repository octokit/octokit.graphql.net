using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible types of a registry package.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RegistryPackageType
    {
        /// <summary>
        /// An npm registry package.
        /// </summary>
        [EnumMember(Value = "NPM")]
        Npm,

        /// <summary>
        /// A rubygems registry package.
        /// </summary>
        [EnumMember(Value = "RUBYGEMS")]
        Rubygems,

        /// <summary>
        /// A maven registry package.
        /// </summary>
        [EnumMember(Value = "MAVEN")]
        Maven,

        /// <summary>
        /// A docker image.
        /// </summary>
        [EnumMember(Value = "DOCKER")]
        Docker,

        /// <summary>
        /// A debian package.
        /// </summary>
        [EnumMember(Value = "DEBIAN")]
        Debian,

        /// <summary>
        /// A nuget package.
        /// </summary>
        [EnumMember(Value = "NUGET")]
        Nuget,

        /// <summary>
        /// A python package.
        /// </summary>
        [EnumMember(Value = "PYTHON")]
        Python,
    }
}