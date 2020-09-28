using System.Collections.Generic;
using OOP_Model;
using Python.Model;

namespace Python.AST.Map.Test.Helpers.Models
{
    public static class StatementBuilder
    {
        private const int DefaultLineNumber = 1;


        public static AssignStatement BuildAssignStatement(IEnumerable<IExpression> leftPart,
            IEnumerable<IExpression> rightPart)
        {
            var statement = new AssignStatement(DefaultLineNumber);
            statement.SetLeftExpressions(leftPart);
            statement.SetRightExpressions(rightPart);
            return statement;
        }
    }
}