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
                @"[EnumMember(Value = ""VALUE"")]
        Value,

        [EnumMember(Value = ""VALUE_2"")]
        Value2,");

            var model = new TypeModel
            {
                Name = "TestEnum",
                Kind = TypeKind.Enum,
                EnumValues = new[]
                {
                    new EnumValueModel { Name = "VALUE" },
                    new EnumValueModel { Name = "VALUE_2" },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Enum_DocComment()
        {
            var expected = @"using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Test
{
    /// <summary>
    /// Testing if doc comments are generated.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TestEnum
    {
    }
}";

            var model = new TypeModel
            {
                Name = "TestEnum",
                Kind = TypeKind.Enum,
                Description = "Testing if doc comments are generated.",
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Enum_Value_DocComment()
        {
            var expected = @"using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Test
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TestEnum
    {
        /// <summary>
        /// Testing if doc comments are generated.
        /// </summary>
        [EnumMember(Value = ""VALUE"")]
        Value,
    }
}";

            var model = new TypeModel
            {
                Name = "TestEnum",
                Kind = TypeKind.Enum,
                EnumValues = new[]
                {
                    new EnumValueModel
                    {
                        Name = "VALUE",
                        Description = "Testing if doc comments are generated.",
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }
    }
}
