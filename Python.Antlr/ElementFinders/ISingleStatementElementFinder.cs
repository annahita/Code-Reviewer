using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Tree;
using Python.Antlr.Extension;
using Python.Antlr.Path;

namespace Python.Antlr.ElementFinders
{
    public interface ISingleStatementElementFinder : IElementFinder
    {
        IEnumerable<IIdentifierElementFinder> FindVariables();
        IEnumerable<IMethodInvocationElementFinder> FindMethodInvocations();
    }

    public class SinglePartStatementElementFinder : ElementFinderBase, ISingleStatementElementFinder
    {
        public SinglePartStatementElementFinder(IParseTree tree, PythonParser parser) : base(tree, parser)
        {
        }

        public IEnumerable<IIdentifierElementFinder> FindVariables()
        {
            var variableTrees = FilterByVariablesThatHveIdentifier(FindExpressions());
            return variableTrees.Select(variableTree => new IdentifierElementFinder(variableTree, _parser));
        }

        public IEnumerable<IMethodInvocationElementFinder> FindMethodInvocations()
        {
            var functionCallTrees = FilterByFunctionCall(FindExpressionElement());
            return functionCallTrees.Select(functionCallTree =>
                new MethodInvocationElementFinder(functionCallTree, _parser));
        }

        private IEnumerable<IParseTree> FindExpressions()
        {
            return FindInPath(PathConfig.ExprInStatement);
        }

        private IEnumerable<IParseTree> FindExpressionElement()
        {
            return FindInPath(PathConfig.ExprElementAnywhere);
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

        private IEnumerable<IParseTree> FilterByFunctionCall(IEnumerable<IParseTree> trees)
        {
            return trees.FindByContextLabel<PythonParser.Function_callContext>();
        }
    }
}