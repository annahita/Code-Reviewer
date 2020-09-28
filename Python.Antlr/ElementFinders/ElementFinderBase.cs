using System.Collections.Generic;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime.Tree.Xpath;
using Python.Antlr.Extension;
using Python.Antlr.Path;

namespace Python.Antlr.ElementFinders
{
    public interface IElementFinder
    {
        int GetLineNumber();
        int GetEndLineNumber();
    }

    public abstract class ElementFinderBase : IElementFinder
    {
        protected readonly PythonParser _parser;
        protected readonly IParseTree _tree;

        protected ElementFinderBase(IParseTree tree, PythonParser parser)
        {
            _tree = tree;
            _parser = parser;
        }

        public int GetLineNumber()
        {
            return _tree.LineNumber();
        }

        public int GetEndLineNumber()
        {
            return _tree.StopLineNumber();
        }

        internal IEnumerable<IParseTree> FindInPath(ParserPath path)
        {
            return XPath.FindAll(_tree, path.Path, _parser);
        }

        internal IEnumerable<IParseTree> FindInPath(IParseTree tree, ParserPath path)
        {
            return XPath.FindAll(tree, path.Path, _parser);
        }
    }
}