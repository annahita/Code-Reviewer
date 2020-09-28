using System.Linq;
using Antlr4.Runtime.Tree;
using Python.Antlr.Path;

namespace Python.Antlr.ElementFinders
{
    public interface IParameterElementFinder : IElementFinder
    {
        string FindParameterName();
    }

    public class ParameterElementFinder : ElementFinderBase, IParameterElementFinder
    {
        public ParameterElementFinder(IParseTree tree, PythonParser parser) : base(tree, parser)
        {
        }

        public string FindParameterName()
        {
            var nameTree = FindInPath(PathConfig.ParameterName).FirstOrDefault();
            return nameTree.GetText();
        }
    }
}