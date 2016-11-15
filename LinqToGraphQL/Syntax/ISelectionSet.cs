using System;
using System.Collections.Generic;

namespace LinqToGraphQL.Syntax
{
    public interface ISelectionSet : ISyntaxNode
    {
        IList<ISyntaxNode> Selections { get; }
    }
}
