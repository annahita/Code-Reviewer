using System.Collections.Generic;

namespace Python.Antlr.Test.PythonCodeTemplates
{
    public static partial class PythonCodeGenerator
    {
        private const string EmptyFunctionBody = "pass";
        private const string StaticMethodDecorator = "staticmethod";

        public static string GenerateFunction(string functionName, string[] parameters, string functionBody,
            int indent = DefaultFunctionsIndent)
        {
            return GenerateIndents(indent) +
                   $"def {functionName}({string.Join(",", parameters)}):\r\n{functionBody}";
        }

        public static string GenerateEmptyFunction(string functionName, int indent = DefaultFunctionsIndent)
        {
            var functionBody = GenerateIndents(indent + 1) + EmptyFunctionBody;
            return GenerateIndents(indent) + $"def {functionName}():\r\n{functionBody}\r\n";
        }

        public static string GenerateFunctionBody(IEnumerable<string> statements)
        {
            return string.Join(NewLine, statements);
        }
    }
}