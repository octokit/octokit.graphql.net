using System;
using System.Collections.Generic;
using System.Text;

namespace Octokit.GraphQL.Core.Syntax
{
    public class VariableDefinition
    {
        public VariableDefinition(Type type, bool isNullable, string name)
        {
            Type = ToTypeName(type, isNullable);
            Name = name;
        }

        public string Type { get; }
        public string Name { get; }

        public static string ToTypeName(Type type, bool isNullable)
        {
            var name = type.Name;

            if (type == typeof(int))
            {
                name = "Int";
            }
            else if (type == typeof(double))
            {
                name = "Float";
            }
            else if (type == typeof(bool))
            {
                name = "Boolean";
            }

            return name + (isNullable ? "" : "!");
        }
    }
}
