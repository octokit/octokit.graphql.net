namespace Octokit.GraphQL.Core.Syntax
{
    public class FragmentUsage : ISyntaxNode
    {
        public string Name { get; }

        public FragmentUsage(string name)
        {
            Name = name;
        }
    }
}