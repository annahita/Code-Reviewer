using System.Collections.Generic;
using Python.Antlr.ElementFinders;

namespace Python.AST.Map.Test.Helpers
{
    public class FakeIMethodBodyElementFinder : IMethodBodyElementFinder
    {
        private const int Line = 1;

        private readonly IEnumerable<IAssignStatementElementFinder> _assignStatement;
        private readonly int _countOfDirectStatementsInBody;
        private readonly IEnumerable<IEmbeddedStatementElementFinder> _embeddedStatement;
        private readonly IEnumerable<ISingleStatementElementFinder> _returnStatements;
        private readonly IEnumerable<ISingleStatementElementFinder> _statements;

        public FakeIMethodBodyElementFinder(
            int countOfDirectStatementsInBody = 1)
        {
            _embeddedStatement = new List<IEmbeddedStatementElementFinder>();
            _countOfDirectStatementsInBody = countOfDirectStatementsInBody;
            _assignStatement = new List<IAssignStatementElementFinder>();
            _statements = new List<ISingleStatementElementFinder>();
            _returnStatements = new List<ISingleStatementElementFinder>();
        }

        public FakeIMethodBodyElementFinder(IEnumerable<IEmbeddedStatementElementFinder> embeddedStatement,
            int countOfDirectStatementsInBody = 1)
        {
            _embeddedStatement = embeddedStatement;
            _countOfDirectStatementsInBody = countOfDirectStatementsInBody;
            _assignStatement = new List<IAssignStatementElementFinder>();
            _statements = new List<ISingleStatementElementFinder>();
            _returnStatements = new List<ISingleStatementElementFinder>();
        }

        public FakeIMethodBodyElementFinder(IEnumerable<IAssignStatementElementFinder> assignStatement,
            int countOfDirectStatementsInBody = 1)
        {
            _assignStatement = assignStatement;
            _countOfDirectStatementsInBody = countOfDirectStatementsInBody;

            _embeddedStatement = new List<IEmbeddedStatementElementFinder>();
            _statements = new List<ISingleStatementElementFinder>();
            _returnStatements = new List<ISingleStatementElementFinder>();
        }

        public FakeIMethodBodyElementFinder(IEnumerable<ISingleStatementElementFinder> singleStatements,
            IEnumerable<ISingleStatementElementFinder> returnStatements,
            int countOfDirectStatementsInBody = 1)
        {
            _statements = singleStatements;
            _returnStatements = returnStatements;
            _countOfDirectStatementsInBody = countOfDirectStatementsInBody;
            _embeddedStatement = new List<IEmbeddedStatementElementFinder>();
            _assignStatement = new List<IAssignStatementElementFinder>();
        }

        public IEnumerable<IEmbeddedStatementElementFinder> FindEmbeddedStatements()
        {
            return _embeddedStatement;
        }

        public IEnumerable<IAssignStatementElementFinder> FindAssignStatements()
        {
            return _assignStatement;
        }

        public IEnumerable<ISingleStatementElementFinder> FindSingleStatements()
        {
            return _statements;
        }

        public IEnumerable<ISingleStatementElementFinder> FindReturnStatements()
        {
            return _returnStatements;
        }

        public int CountOfDirectStatementsInBody()
        {
            return _countOfDirectStatementsInBody;
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