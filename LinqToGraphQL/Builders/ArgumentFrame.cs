using System;
using System.Collections.Generic;
using GraphQLParser.AST;

namespace LinqToGraphQL.Builders
{
    internal class ArgumentFrame : Frame
    {
        public ArgumentFrame(ASTNode parent, string name)
            : base (new GraphQLArgument(name))
        {
            var field = parent as GraphQLFieldSelection;

            if (field != null)
            {
                var args = (List<GraphQLArgument>)field.Arguments;

                if (args == null)
                {
                    field.Arguments = args = new List<GraphQLArgument>();
                }

                args.Add((GraphQLArgument)Node);
            }
        }

        public void SetValue(object value)
        {
            ((GraphQLArgument)Node).Value = GraphQLValue.Create(value);
        }
    }
}
