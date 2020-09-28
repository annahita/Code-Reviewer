using System.Collections.Generic;
using System.Linq;

namespace Python.Antlr.Test.PythonCodeTemplates
{
    public static partial class PythonCodeGenerator
    {
        private const string CommaSeparator = ",";
        private const char Indent = ' ';
        private const string NewLine = "\r\n";
        private const int DefaultClassIndent = 0;
        private const int DefaultFunctionsIndent = 1;
        private const int DefaultStatementIndent = 2;


        private static string JoinByComma(string[] names)
        {
            return string.Join(CommaSeparator, names);
        }

        private static string JoinByLines(IEnumerable<string> lines)
        {
            return string.Join("\r\n", lines.ToArray());
        }

        private static string GenerateIndents(int n)
        {
            return new string(Indent, n * 4);
        }
    }
}