using System.Collections.Generic;
using OOP_Model;

namespace Python.Model
{
    public class SinglePartStatement : EntityBase, ISimpleStatement
    {
        public SinglePartStatement(int lineNumber) : base(lineNumber)
        {
        }

        public IEnumerable<IExpression> Expressions { get; private set; } = new List<IExpression>();

        public void SetExpressions(IEnumerable<IExpression> expressions)
        {
            Expressions = expressions;
        }
    }
}