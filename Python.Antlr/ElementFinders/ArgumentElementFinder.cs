using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Tree;
using Python.Antlr.Extension;
using Python.Antlr.Path;

namespace Python.Antlr.ElementFinders
{
    public interface IArgumentElementFinder : IElementFinder
    {
        IEnumerable<IMethodInvocationElementFinder> FindArgumentsWithMethodInvocationType();
        IEnumerable<IIdentifierElementFinder> FindArgumentsWithVariableType();
    }

    public class ArgumentElementFinder : ElementFinderBase, IArgumentElementFinder
    {
        public ArgumentElementFinder(IParseTree tree, PythonParser parser) : base(tree, parser)
        {
        }

        public IEnumerable<IMethodInvocationElementFinder> FindArgumentsWithMethodInvocationType()
        {
            var methodInvocationTrees = FilterByFunctionCall(FindFirstLevelArgumentExpressions());
            return methodInvocationTrees.Select(methodInvocationTree =>
                new MethodInvocationElementFinder(methodInvocationTree, _parser));
        }

        public IEnumerable<IIdentifierElementFinder> FindArgumentsWithVariableType()
        {
            var identifierTrees = FilterByVariablesThatHveIdentifier(FindFirstLevelArgumentExpressions());
            return identifierTrees.Select(identifierTree => new IdentifierElementFinder(identifierTree, _parser));
        }


        private IEnumerable<IParseTree> FindFirstLevelArgumentExpressions()
        {
            var arguments = FindInPath(PathConfig.Arguments);
            return arguments.Select(argument => FindInPath(argument, PathConfig.ExprElementAnywhere).FirstOrDefault());
        }

        private IEnumerable<IParseTree> FilterByFunctionCall(IEnumerable<IParseTree> trees)
        {
            return trees.FindByContextLabel<PythonParser.Function_callContext>();
        }

        private IEnumerable<IParseTree> FilterByVariablesThatHveIdentifier(IEnumerable<IParseTree> trees)
        {
            var variables = trees.FindByContextLabel<PythonParser.Variable_idContext>();
            return variables.Where(HasIdentifier);
        }

        private bool HasIdentifier(PythonParser.Variable_idContext exprTree)
        {
            return FindInPath(exprTree, PathConfig.ExprElementIdentifier).Any();
        }
    }
}