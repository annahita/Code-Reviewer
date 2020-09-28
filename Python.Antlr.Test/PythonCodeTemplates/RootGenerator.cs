using System.Collections.Generic;
using System.Linq;

namespace Python.Antlr.Test.PythonCodeTemplates
{
    public static partial class PythonCodeGenerator
    {
        public static string GenerateRoot(IEnumerable<string> classNames)
        {
            var classes = classNames.Select(GenerateDefaultClass);
            return JoinByLines(classes);
        }
    }
}