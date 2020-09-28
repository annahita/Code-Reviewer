using System.Collections.Generic;
using System.Linq;

namespace Python.Antlr.Test.PythonCodeTemplates
{
    public static partial class PythonCodeGenerator
    {
        private const string DefaultClassConstructor = "__init__";
        private const string DefaultClassBody = "   pass";

        private static readonly string[] NullArgument = { };

        public static string GenerateDefaultClass(string className)
        {
            return GenerateIndents(DefaultClassIndent) + GenerateClass(className, NullArgument, DefaultClassBody);
        }

        public static string GenerateClassBody(IEnumerable<(string, string)> assignmentParts,
            IEnumerable<string> methodNames)
        {
            var assignmentStatements = assignmentParts.Select(assignmentPart =>
                GenerateAssignStatement(assignmentPart.Item1, assignmentPart.Item2, 1));
            var constructor = GenerateEmptyFunction(DefaultClassConstructor);
            var methods = methodNames.Select(methodName => GenerateEmptyFunction(methodName));

            return $"{JoinByLines(assignmentStatements)}\r\n{constructor}\r\n{JoinByLines(methods)}\r\n";
        }

        public static string GenerateClass(string className, string[] arguments, string classBody)
        {
            return GenerateIndents(DefaultClassIndent) +
                   $"class {className}({JoinByComma(arguments)}):\r\n{classBody}\r\n";
        }
    }
}