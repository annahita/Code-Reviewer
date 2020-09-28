using System.Collections.Generic;
using System.Linq;
using OOP_Model;
using Python.Model;

namespace Python.AST.Map.Test.Helpers.Models
{
    public static class MethodBuilder
    {
        private const int DefaultLineNumber = 1;

        public static MethodBody BuildMethodBody(IEnumerable<ISimpleStatement> statements)
        {
            var body = new MethodBody(DefaultLineNumber, DefaultLineNumber, statements.Count());
            body.SetSimpleStatements(statements);
            return body;
        }

        public static MethodDeclaration BuildMethod(string name, IEnumerable<ParameterDeclaration> parameters,
            ParameterDeclaration instancePointer)
        {
            var method = new MethodDeclaration(DefaultLineNumber);
            method.SetName(name);
            method.SetInstancePointer(instancePointer);
            method.SetParameters(parameters);
            return method;
        }
    }
}