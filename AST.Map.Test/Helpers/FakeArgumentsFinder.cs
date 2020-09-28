using System.Collections.Generic;
using Python.Antlr.ElementFinders;

namespace Python.AST.Map.Test.Helpers
{
    public class FakeArgumentsFinder : IArgumentElementFinder
    {
        private const int Line = 1;
        private readonly string[] _argumentWithMethodInvocation;
        private readonly string[] _argumentWithVariableType;

        public FakeArgumentsFinder(string[] argumentWithMethodInvocation, string[] argumentWithVariableType)
        {
            _argumentWithVariableType = argumentWithVariableType;
            _argumentWithMethodInvocation = argumentWithMethodInvocation;
        }

        public IEnumerable<IIdentifierElementFinder> FindArgumentsWithVariableType()
        {
            return _argumentWithVariableType != null
                ? new[] {new FakeIdentifierElementFinder(_argumentWithVariableType)}
                : new IIdentifierElementFinder[] { };
        }

        public IEnumerable<IMethodInvocationElementFinder> FindArgumentsWithMethodInvocationType()
        {
            return _argumentWithMethodInvocation != null
                ? new[] {new FakeMethodInvocationElementFinder(_argumentWithMethodInvocation)}
                : new IMethodInvocationElementFinder[] { };
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