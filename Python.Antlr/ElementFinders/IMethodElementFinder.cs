using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Tree;
using Python.Antlr.Path;

namespace Python.Antlr.ElementFinders
{
    public interface IMethodElementFinder : IElementFinder
    {
        string FindName();
        IEnumerable<IParameterElementFinder> FindParameters();
        IMethodBodyElementFinder FindBody();
    }

    public class MethodElementFinder : ElementFinderBase, IMethodElementFinder
    {
        public MethodElementFinder(IParseTree tree, PythonParser parser) : base(tree, parser)
        {
        }

        public string FindName()
        {
            var nameTree = FindInPath(PathConfig.FunctionName).FirstOrDefault();
            return nameTree.GetText();
        }

        public IEnumerable<IParameterElementFinder> FindParameters()
        {
            var parameterTrees = FindInPath(PathConfig.FunctionParameters);
            return parameterTrees.Select(parameterTree => new ParameterElementFinder(parameterTree, _parser));
        }

        public IMethodBodyElementFinder FindBody()
        {
            var functionBlockTree = FindInPath(PathConfig.FunctionBlock).FirstOrDefault();
            return new MethodBodyElementFinder(functionBlockTree, _parser);
        }
    }
}