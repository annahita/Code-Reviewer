using Antlr4.Runtime;

namespace Python.Antlr.Test
{
    public static class TestParser
    {
        public static PythonParser GetParser(string pythonCodes)
        {
            var inputStream = new AntlrInputStream(pythonCodes);
            var lexer = new PythonLexer(inputStream);
            var tokens = new CommonTokenStream(lexer);
            return new PythonParser(tokens);
        }
    }
}