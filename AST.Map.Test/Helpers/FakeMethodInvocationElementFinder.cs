using Python.Antlr.ElementFinders;

namespace Python.AST.Map.Test.Helpers
{
    public class FakeMethodInvocationElementFinder : IMethodInvocationElementFinder
    {
        private const int Line = 1;
        private readonly string[] _argumentWithMethodInvocation;
        private readonly string[] _argumentWithVariableType;
        private readonly string[] _invokedMethodIdentifier;

        public FakeMethodInvocationElementFinder(string[] invokedMethodIdentifier, string[] argumentWithVariableType,
            string[] argumentWithMethodInvocation)
        {
            _argumentWithVariableType = argumentWithVariableType;
            _argumentWithMethodInvocation = argumentWithMethodInvocation;
            _invokedMethodIdentifier = invokedMethodIdentifier;
        }

        public FakeMethodInvocationElementFinder(string[] invokedMethodIdentifier, string[] argumentWithVariableType)
        {
            _argumentWithVariableType = argumentWithVariableType;
            _invokedMethodIdentifier = invokedMethodIdentifier;
            _argumentWithMethodInvocation = null;
        }

        public FakeMethodInvocationElementFinder(string[] invokedMethodIdentifier)
        {
            _argumentWithVariableType = null;
            _argumentWithMethodInvocation = null;
            _invokedMethodIdentifier = invokedMethodIdentifier;
        }

        public IArgumentElementFinder FindArgumentCollection()
        {
            return new FakeArgumentsFinder(_argumentWithMethodInvocation, _argumentWithVariableType);
        }

        public IIdentifierElementFinder FindIdentifier()
        {
            return new FakeIdentifierElementFinder(_invokedMethodIdentifier);
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