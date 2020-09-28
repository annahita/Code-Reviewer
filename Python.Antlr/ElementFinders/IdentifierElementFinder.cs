using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Tree;
using Python.Antlr.Path;

namespace Python.Antlr.ElementFinders
{
    public interface IIdentifierElementFinder : IElementFinder
    {
        IEnumerable<string> FindIdentifier();
    }

    public class IdentifierElementFinder : ElementFinderBase, IIdentifierElementFinder
    {
        public IdentifierElementFinder(IParseTree tree, PythonParser parser) : base(tree, parser)
        {
        }

        public IEnumerable<string> FindIdentifier()
        {
            var identifierTrees = FindInPath(PathConfig.ExprElementIdentifier);
            return identifierTrees.Select(FindName).ToList();
        }

        private string FindName(IParseTree tree)
        {
            var nameTree = FindInPath(tree, PathConfig.Name).FirstOrDefault();
            return nameTree.GetText();
        }
    }
}