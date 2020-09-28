using Antlr4.Runtime.Tree;
using Python.Antlr.ElementFinders;

namespace Python.Antlr.Test.ElementFinders.Fakes
{
    public class FakeBaseElementFinder : ElementFinderBase
    {
        public FakeBaseElementFinder(IParseTree tree, PythonParser parser) : base(tree, parser)
        {
        }
    }
}