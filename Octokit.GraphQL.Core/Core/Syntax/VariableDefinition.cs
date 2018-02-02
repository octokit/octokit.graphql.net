using System;
using System.Collections.Generic;
using System.Text;

namespace Octokit.GraphQL.Core.Syntax
{
    public class VariableDefinition
    {
        public VariableDefinition(Type type, string name)
        {
            Type = ToTypeName(type);
            Name = name;
        }

        public string Type { get; }
        public string Name { get; }

        public static string ToTypeName(Type type)
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

            return type.Name;
        }
    }
}
