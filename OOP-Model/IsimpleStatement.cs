using System.Collections.Generic;

namespace OOP_Model
{
    public interface ISimpleStatement
    {
        IEnumerable<IExpression> Expressions { get; }
    }
}