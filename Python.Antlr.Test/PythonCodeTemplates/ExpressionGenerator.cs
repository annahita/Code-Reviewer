namespace Python.Antlr.Test.PythonCodeTemplates
{
    public static partial class PythonCodeGenerator
    {
        private const string DefaultFunctionName = "defaultFunction";

        public static string GenerateDefaultMethodInvocation(
            int indent = DefaultStatementIndent)
        {
            return GenerateIndents(indent) + $"{DefaultFunctionName}()";
        }


        public static string GenerateDefaultVariableDeclaration(int indent = DefaultStatementIndent)
        {
            return GenerateIndents(indent) + "DefaultVariable=100";
        }


        public static string GenerateMethodInvocation(string methodIdentifier, string[] arguments,
            int indent = DefaultStatementIndent)
        {
            return GenerateIndents(indent) + $"{methodIdentifier}({JoinByComma(arguments)})";
        }
    }
}