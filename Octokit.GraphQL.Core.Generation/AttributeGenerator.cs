namespace Octokit.GraphQL.Core.Generation
{
    internal static class AttributeGenerator
    {
        public static string GenerateObsoleteAttribute(string reason)
        {
            if (string.IsNullOrEmpty(reason))
            {
                return "[Obsolete]";
            }

            return $"[Obsolete(@\"{reason}\")]";
        }
    }
}