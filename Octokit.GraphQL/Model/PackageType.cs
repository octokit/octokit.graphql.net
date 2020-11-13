using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible types of a package.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PackageType
    {
        /// <summary>
        /// An npm package.
        /// </summary>
        [EnumMember(Value = "NPM")]
        Npm,

        /// <summary>
        /// A rubygems package.
        /// </summary>
        [EnumMember(Value = "RUBYGEMS")]
        Rubygems,

        /// <summary>
        /// A maven package.
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
        [EnumMember(Value = "PYPI")]
        Pypi,
    }
}