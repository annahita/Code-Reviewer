using System.Collections.Generic;
using System.Linq;

namespace Python.Antlr.Test.PythonCodeTemplates
{
    public partial class PythonCodeGenerator
    {
        private static string GenerateCompoundStatementBody(IEnumerable<string> statements, int indent = 0)
        {
            return string.Join(NewLine, statements.Select(a => GenerateIndents(indent) + a));
        }

        public static string GenerateDefaultCompoundStatement(int indent = 0)
        {
            const string statement = "i=i+1";
            var body = GenerateCompoundStatementBody(new[] {statement}, indent + 1);
            const string condition = "i>0";

            return GenerateIndents(indent) + $"while {condition} :\r\n{body}";
        }

        public static string GenerateDefaultNestedCompoundStatement(
            string[] nestedCompoundStatement, int indent = 0)
        {
            return GenerateDefaultNestedCompoundStatement(new string[] { }, nestedCompoundStatement, indent);
        }

        public static string GenerateDefaultNestedCompoundStatement(string[] simpleStatements,
            string[] nestedCompoundStatement, int indent = 0)
        {
            var simpleStatement = GenerateCompoundStatementBody(simpleStatements);
            var compoundStatement = GenerateCompoundStatementBody(nestedCompoundStatement);
            var body = simpleStatement + NewLine + compoundStatement;

            const string condition = "i>0";
            return GenerateIndents(indent) + $"while {condition} :\r\n{body}";
        }
    }
}