using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Python.Antlr.ElementFinders;

namespace Python.Antlr
{
    public interface IParserBuilder
    {
        void Build(string code);
        IRootElementFinder GetParseElementFinder();
    }

    public class ParserBuilder : IParserBuilder
    {
        private PythonParser _parser;
        private IParseTree _tree;

        public void Build(string code)
        {
            var inputStream = new AntlrInputStream(code);
            var lexer = new PythonLexer(inputStream);
            var tokens = new CommonTokenStream(lexer);
            _parser = new PythonParser(tokens);
            _tree = _parser.file_input();
        }

        public IRootElementFinder GetParseElementFinder()
        {
            return new RootElementFinder(_tree, _parser);
        }
    }
}