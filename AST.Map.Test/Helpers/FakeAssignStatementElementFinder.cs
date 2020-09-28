using System.Collections.Generic;
using Python.Antlr.ElementFinders;

namespace Python.AST.Map.Test.Helpers
{
    public class FakeAssignStatementElementFinder : IAssignStatementElementFinder
    {
        private const int Line = 1;
        private readonly string[] _leftPartVariableIdentifier;
        private readonly string[] _rightPartInvokedMethodIdentifier;
        private readonly string[] _rightPartVariableIdentifier;

        public FakeAssignStatementElementFinder(string[] leftPartVariableIdentifier,
            string[] rightPartVariableIdentifier, string[] rightPartInvokedMethodIdentifier)
        {
            _leftPartVariableIdentifier = leftPartVariableIdentifier;
            _rightPartVariableIdentifier = rightPartVariableIdentifier;
            _rightPartInvokedMethodIdentifier = rightPartInvokedMethodIdentifier;
        }

        public FakeAssignStatementElementFinder(string[] leftPart, string[] rightPart)
        {
            _leftPartVariableIdentifier = leftPart;
            _rightPartVariableIdentifier = rightPart;
            _rightPartInvokedMethodIdentifier = null;
        }

        public IEnumerable<IIdentifierElementFinder> FindVariablesInLeftPart()
        {
            return new[] {new FakeIdentifierElementFinder(_leftPartVariableIdentifier)};
        }

        public IEnumerable<IIdentifierElementFinder> FindVariablesInRightPart()
        {
            return new[] {new FakeIdentifierElementFinder(_rightPartVariableIdentifier)};
        }

        public IEnumerable<IMethodInvocationElementFinder> FindMethodInvocations()
        {
            if (_rightPartInvokedMethodIdentifier != null)
                return new[] {new FakeMethodInvocationElementFinder(_rightPartInvokedMethodIdentifier)};

            return new List<IMethodInvocationElementFinder>();
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