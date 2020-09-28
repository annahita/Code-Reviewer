using System.Collections.Generic;
using Python.Antlr.ElementFinders;

namespace Python.AST.Map.Test.Helpers
{
    public class FakeEmbeddedStatementElementFinder : IEmbeddedStatementElementFinder
    {
        private const int Line = 1;
        private readonly int _countOfDirectChildren;
        private readonly int _countOfNestedEmbeddedStatements;

        public FakeEmbeddedStatementElementFinder(int countOfNestedEmbeddedStatements = 0,
            int countOfDirectChildren = 1)
        {
            _countOfNestedEmbeddedStatements = countOfNestedEmbeddedStatements;
            _countOfDirectChildren = countOfDirectChildren;
        }

        public IEnumerable<IEmbeddedStatementElementFinder> FindNestedEmbeddedStatements()
        {
            var nestedStatements = new List<IEmbeddedStatementElementFinder>();
            if (_countOfNestedEmbeddedStatements > 0)
                nestedStatements.Add(new FakeEmbeddedStatementElementFinder(_countOfNestedEmbeddedStatements - 1));

            return nestedStatements;
        }

        public int CountOfDirectStatementsInBody()
        {
            return _countOfDirectChildren;
        }


        public int GetLineNumber()
        {
            return Line;
        }

        public int GetEndLineNumber()
        {
            return Line;
        }
    }
}