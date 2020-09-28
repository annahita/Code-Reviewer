namespace Python.Antlr.Test.PythonCodeTemplates
{
    public static partial class PythonCodeGenerator
    {
        private const string DefaultVariableId = "defaultLocalVar";
        private const string DefaultValue = "5";

        public static string GenerateAssignStatement(string left, string right,
            int indent = DefaultStatementIndent)
        {
            return GenerateIndents(indent) + $"{left}={right}";
        }

        public static string GenerateDefaultAssignmentStatement(
            int indent = DefaultStatementIndent)
        {
            return GenerateIndents(indent) + $"{DefaultVariableId}={DefaultValue}";
        }

        public static string GenerateReturnStatement(string expression,
            int indent = DefaultStatementIndent)
        {
            return GenerateIndents(indent) + $"return {expression}";
        }

        public static string GenerateDefaultReturnStatement(
            int indent = DefaultStatementIndent)
        {
            return GenerateIndents(indent) + $"return {DefaultVariableId}";
        }

        public static string GenerateDefaultSingleStatement(
            int indent = DefaultStatementIndent)
        {
            return GenerateIndents(indent) + $"{GenerateDefaultMethodInvocation()}";
        }
    }
}