using Octokit.GraphQL.Core.Generation.Models;
using Octokit.GraphQL.Core.Generation.Utilities;
using Octokit.GraphQL.Core.Introspection;
using Xunit;

namespace Octokit.GraphQL.Core.Generation.UnitTests.Utilities
{
    public class TypeUtilitiesTests
    {
        [Fact]
        public void ReduceType_Should_Not_Reduce_NonNull_Int()
        {
            var result = TypeUtilities.ReduceType(TypeModel.NonNull(TypeModel.Int()));

            Assert.Equal(TypeKind.NonNull, result.Kind);
            Assert.Equal(TypeKind.Scalar, result.OfType.Kind);
            Assert.Equal("Int", result.OfType.Name);
        }

        [Fact]
        public void ReduceType_Should_Not_Reduce_NonNull_Float()
        {
            var result = TypeUtilities.ReduceType(TypeModel.NonNull(TypeModel.Float()));

            Assert.Equal(TypeKind.NonNull, result.Kind);
            Assert.Equal(TypeKind.Scalar, result.OfType.Kind);
            Assert.Equal("Float", result.OfType.Name);
        }

        [Fact]
        public void ReduceType_Should_Not_Reduce_NonNull_Bool()
        {
            var result = TypeUtilities.ReduceType(TypeModel.NonNull(TypeModel.Boolean()));

            Assert.Equal(TypeKind.NonNull, result.Kind);
            Assert.Equal(TypeKind.Scalar, result.OfType.Kind);
            Assert.Equal("Boolean", result.OfType.Name);
        }

        [Fact]
        public void ReduceType_Should_Not_Reduce_NonEnum()
        {
            var result = TypeUtilities.ReduceType(TypeModel.NonNull(TypeModel.Enum("Foo")));

            Assert.Equal(TypeKind.NonNull, result.Kind);
            Assert.Equal(TypeKind.Enum, result.OfType.Kind);
            Assert.Equal("Foo", result.OfType.Name);
        }

        [Fact]
        public void ReduceType_Should_Reduce_NonNull_String()
        {
            var result = TypeUtilities.ReduceType(TypeModel.NonNull(TypeModel.String()));

            Assert.Equal(TypeKind.Scalar, result.Kind);
            Assert.Equal("String", result.Name);
        }

        [Fact]
        public void ReduceType_Should_Reduce_NonNull_Object()
        {
            var result = TypeUtilities.ReduceType(TypeModel.NonNull(TypeModel.Object("Foo")));

            Assert.Equal(TypeKind.Object, result.Kind);
        }

        [Fact]
        public void ReduceType_Should_Reduce_NonNull_List()
        {
            var result = TypeUtilities.ReduceType(TypeModel.NonNull(TypeModel.List(TypeModel.Object("Foo"))));

            Assert.Equal(TypeKind.List, result.Kind);
        }

        [Fact]
        public void ReduceType_Should_Reduce_List_Of_NonNull_String()
        {
            var result = TypeUtilities.ReduceType(TypeModel.List(TypeModel.NonNull(TypeModel.String())));

            Assert.Equal(TypeKind.List, result.Kind);
            Assert.Equal(TypeKind.Scalar, result.OfType.Kind);
            Assert.Equal("String", result.OfType.Name);
        }

        [Fact]
        public void ReduceType_Should_Reduce_NonNull_List_Of_NonNull_Object()
        {
            var result = TypeUtilities.ReduceType(TypeModel.NonNull(TypeModel.List(TypeModel.NonNull(TypeModel.Object("Foo")))));

            Assert.Equal(TypeKind.List, result.Kind);
            Assert.Equal(TypeKind.Object, result.OfType.Kind);
            Assert.Equal("Foo", result.OfType.Name);
        }
    }
}
