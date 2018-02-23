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
            if (type == typeof(int))
            {
                return "Int";
            }
            else if (type == typeof(double))
            {
                return "Float";
            }
            else if (type == typeof(bool))
            {
                return "Boolean";
            }

            return type.Name + (isNullable ? "" : "!");
        }
    }
}
