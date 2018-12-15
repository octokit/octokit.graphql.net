using System;
using System.Text;
using System.Xml;

namespace Octokit.GraphQL.Core.Generation
{
    internal static class DocCommentGenerator
    {
        public static void GenerateMemberSummary(string summary, int indentation, StringBuilder builder)
        {
            if (!string.IsNullOrWhiteSpace(summary))
            {
                GenerateSummary(summary, indentation, new StringBuilder());
            }
            else
            {
                var indent = new string(' ', indentation);
                builder.Append(indent);
                builder.AppendLine("[SuppressMessage(\"System.Diagnostics\", \"CS1591\", Justification = \"Source did not provide detail\")]");
            }
        }

        public static void GenerateSummary(string summary, int indentation, StringBuilder builder)
        {
            if (!string.IsNullOrWhiteSpace(summary))
            {
                var indent = new string(' ', indentation);
                builder.Append(indent);
                builder.AppendLine("/// <summary>");
                GenerateCommentBlock(summary, indent, builder);
                builder.Append(indent);
                builder.AppendLine(@"/// </summary>");
            }
        }

        private static void GenerateCommentBlock(string text, string indent, StringBuilder builder)
        {
            text = text.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");

            foreach (var line in text.Split('\r', '\n'))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    builder.Append(indent);
                    builder.Append("/// ");
                    builder.AppendLine(line);
                }
            }
        }
    }
}
