using System;

namespace Octokit.GraphQL.Core
{
    internal interface IArg
    {
        Type Type { get; }
        string VariableName { get; }
        object Value { get; }
    }
}
