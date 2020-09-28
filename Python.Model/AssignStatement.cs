using System.Collections.Generic;
using System.Linq;
using OOP_Model;

namespace Python.Model
{
    public class AssignStatement : EntityBase, ISimpleStatement
    {
        public AssignStatement(int lineNumber) : base(lineNumber)
        {
        }

        public IEnumerable<IExpression> LeftExpressions { get; private set; } = new List<IExpression>();
        public IEnumerable<IExpression> RightExpressions { get; private set; } = new List<IExpression>();

        public IEnumerable<IExpression> Expressions => LeftExpressions.Concat(RightExpressions);

        public void SetLeftExpressions(IEnumerable<IExpression> expressions)
        {
            LeftExpressions = expressions.ToList();
        }

        public void SetRightExpressions(IEnumerable<IExpression> expressions)
        {
            RightExpressions = expressions.ToList();
        }
    }
}