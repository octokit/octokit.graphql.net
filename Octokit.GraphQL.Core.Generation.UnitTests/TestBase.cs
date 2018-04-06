using System;
using System.IO;
using Xunit;

namespace Octokit.GraphQL.Core.Generation.UnitTests
{
    public class TestBase
    {
        protected static void CompareModel(
            string fileName,
            string expected,
            GeneratedFile result,
            string subdirectory = "Model")
        {
            Assert.Equal(Path.Combine(subdirectory, fileName), result.Path);
            Assert.Equal(expected, result.Content, ignoreLineEndingDifferences: true);
        }
    }
}
