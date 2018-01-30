using System;

namespace Octokit.GraphQL
{
    public struct Variable
    {
        private Variable(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public static Variable Var(string name)
        {
            return new Variable(name);
        }
    }
}
