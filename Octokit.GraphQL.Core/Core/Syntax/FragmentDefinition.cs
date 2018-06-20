using System;
using System.Collections.Generic;
using System.Text;

namespace Octokit.GraphQL.Core.Syntax
{
    public class FragmentDefinition: SelectionSet
    {
        public string Type { get; }
        public string Name { get; }

        public FragmentDefinition(Type inputType, string name)
        {
            if (name.ToLowerInvariant() == "on")
            {
                throw new ArgumentException("On is an invalid value a fragment name", nameof(name));
            }

            Type = ToTypeName(inputType);
            Name = name;
        }

        private string ToTypeName(Type type) => type.Name;
    }
}
