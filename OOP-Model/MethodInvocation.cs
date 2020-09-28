using System.Collections.Generic;

namespace OOP_Model
{
    public interface IMethodInvocation : IEntityBase, IExpression
    {
        IIdentifier Identifier { get; }
        IEnumerable<IExpression> Arguments { get; }
        bool IsMemberOfParentClass { get; }
    }
}