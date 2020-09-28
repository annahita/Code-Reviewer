using System.Collections.Generic;
using Python.Antlr.ElementFinders;

namespace Python.AST.Map.Test.Helpers
{
    public class FakeStatementElementFinder : ISingleStatementElementFinder
    {
        private const int Line = 1;
        private readonly string[] _invokedMethodIdentifier;
        private readonly string[] _variableIdentifier;

        public FakeStatementElementFinder(string[] invokedMethodIdentifier, string[] variableIdentifier)
        {
            _invokedMethodIdentifier = invokedMethodIdentifier;
            _variableIdentifier = variableIdentifier;
        }

        public IEnumerable<IIdentifierElementFinder> FindVariables()
        {
            return new[] {new FakeIdentifierElementFinder(_variableIdentifier)};
        }

        public IEnumerable<IMethodInvocationElementFinder> FindMethodInvocations()
        {
            return new[] {new FakeMethodInvocationElementFinder(_invokedMethodIdentifier)};
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