using System;
using LinqToGraphQL.Generation.Models;
using LinqToGraphQL.Introspection;
using Xunit;

namespace LinqToGraphQL.Generation.UnitTests
{
    public class EnumGenerationTests
    {
        const string MemberTemplate = @"using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Test
{{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TestEnum
    {{
        {0}
    }}
}}";

        [Fact]
        public void Generates_Enums()
        {
            var expected = string.Format(
                MemberTemplate,
                @"[EnumMember(Value = ""VALUE_1"")]
        Value1,

        [EnumMember(Value = ""VALUE_2"")]
        Value2,");

            var model = new TypeModel
            {
                Name = "TestEnum",
                Kind = TypeKind.Enum,
                EnumValues = new[]
                {
                    new EnumValueModel { Name = "VALUE_1" },
                    new EnumValueModel { Name = "VALUE_2" },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }
    }
}
