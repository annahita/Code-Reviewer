using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Tree;
using Python.Antlr.Extension;
using Python.Antlr.Path;

namespace Python.Antlr.ElementFinders
{
    public interface IMethodBodyElementFinder : IElementFinder
    {
        IEnumerable<IEmbeddedStatementElementFinder> FindEmbeddedStatements();
        IEnumerable<IAssignStatementElementFinder> FindAssignStatements();
        IEnumerable<ISingleStatementElementFinder> FindSingleStatements();
        IEnumerable<ISingleStatementElementFinder> FindReturnStatements();
        int CountOfDirectStatementsInBody();
    }

    public class MethodBodyElementFinder : ElementFinderBase, IMethodBodyElementFinder
    {
        public MethodBodyElementFinder(IParseTree tree, PythonParser parser) : base(tree, parser)
        {
        }

        public IEnumerable<IEmbeddedStatementElementFinder> FindEmbeddedStatements()
        {
            var compoundStatementTrees = FindInPath(PathConfig.CompoundStatementInFunctionSuite);
            return compoundStatementTrees.Select(compoundStatementTree =>
                new EmbeddedStatementElementFinder(compoundStatementTree, _parser));
        }

        public IEnumerable<IAssignStatementElementFinder> FindAssignStatements()
        {
            var assignStatementTrees = Filter.FilterByAssignStatement(SmallStatesmenAnywhere());
            return assignStatementTrees.Select(assignStatementTree =>
                new AssignStatementElementFinder(assignStatementTree, _parser));
        }

        public IEnumerable<ISingleStatementElementFinder> FindSingleStatements()
        {
            var statementTrees = Filter.FilterBySingleStatement(SmallStatesmenAnywhere());
            return statementTrees.Select(statementTree => new SinglePartStatementElementFinder(statementTree, _parser));
        }

        public IEnumerable<ISingleStatementElementFinder> FindReturnStatements()
        {
            var returnStatementTrees = Filter.FilterByReturnStatement(SmallStatesmenAnywhere());
            return returnStatementTrees.Select(returnStatementTree =>
                new SinglePartStatementElementFinder(returnStatementTree, _parser));
        }

        public int CountOfDirectStatementsInBody()
        {
            var directStatementTrees = FindInPath(PathConfig.FirstLevelStatementsInBlock);
            return directStatementTrees.Count();
        }

        private IEnumerable<IParseTree> SmallStatesmenAnywhere()
        {
            return FindInPath(PathConfig.SmallStatesmenAnywhere);
        }
    }
}