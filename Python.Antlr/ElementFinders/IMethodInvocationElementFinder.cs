using System.Linq;
using Antlr4.Runtime.Tree;
using Python.Antlr.Path;

namespace Python.Antlr.ElementFinders
{
    public interface IMethodInvocationElementFinder : IElementFinder
    {
        IIdentifierElementFinder FindIdentifier();
        IArgumentElementFinder FindArgumentCollection();
    }

    public class MethodInvocationElementFinder : ElementFinderBase, IMethodInvocationElementFinder
    {
        public MethodInvocationElementFinder(IParseTree tree, PythonParser parser) : base(tree, parser)
        {
        }

        public IArgumentElementFinder FindArgumentCollection()
        {
            var argTree = FindInPath(PathConfig.InvokedMethodArgumentCollection).FirstOrDefault();
            return new ArgumentElementFinder(argTree, _parser);
        }

        public IIdentifierElementFinder FindIdentifier()
        {
            return new IdentifierElementFinder(_tree, _parser);
        }
    }
}