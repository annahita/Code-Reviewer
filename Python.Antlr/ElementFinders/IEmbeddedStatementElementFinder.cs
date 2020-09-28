using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Tree;
using Python.Antlr.Path;

namespace Python.Antlr.ElementFinders
{
    public interface IEmbeddedStatementElementFinder : IElementFinder
    {
        IEnumerable<IEmbeddedStatementElementFinder> FindNestedEmbeddedStatements();
        int CountOfDirectStatementsInBody();
    }

    public class EmbeddedStatementElementFinder : ElementFinderBase, IEmbeddedStatementElementFinder
    {
        public EmbeddedStatementElementFinder(IParseTree tree, PythonParser parser) : base(tree, parser)
        {
        }

        public IEnumerable<IEmbeddedStatementElementFinder> FindNestedEmbeddedStatements()
        {
            var compoundStatementTrees = FindInPath(PathConfig.NestedCompoundStatement);
            return compoundStatementTrees.Select(compoundStatementTree =>
                new EmbeddedStatementElementFinder(compoundStatementTree, _parser));
        }

        public int CountOfDirectStatementsInBody()
        {
            var directStatementTrees = FindInPath(PathConfig.FirstLevelStatementsInCompoundStatement);
            return directStatementTrees.Count();
        }
    }
}