using System;

namespace Octokit.GraphQL.Core
{
    public struct Arg<T> : IArg
    {
        private Arg(string name, T value)
        {
            VariableName = name;
            Value = value;
        }

        public string VariableName { get; }
        public T Value { get; }
        Type IArg.Type => typeof(T);
        object IArg.Value => Value;

        public static implicit operator Arg<T>(T value)
        {
            return new Arg<T>(null, value);
        }

        public static implicit operator Arg<T>(Variable variable)
        {
            return new Arg<T>(variable.Name, default(T));
        }
    }
}
