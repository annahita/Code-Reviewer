using System.Collections.Generic;
using System.Linq;
using Python.Antlr.ElementFinders;

namespace Python.AST.Map.Test.Helpers
{
    public class FakeInstanceMethodElementFinder : IMethodElementFinder
    {
        private const int Line = 1;
        private readonly IMethodBodyElementFinder _methodBodyElementFinder;
        private readonly string _name;
        private readonly IEnumerable<string> _parameters;

        public FakeInstanceMethodElementFinder(string name, IEnumerable<string> parameters)
        {
            _name = name;
            _parameters = parameters;
            _methodBodyElementFinder = new FakeIMethodBodyElementFinder();
        }

        public FakeInstanceMethodElementFinder(string name, IEnumerable<string> parameters,
            IMethodBodyElementFinder methodBodyElementFinder)
        {
            _name = name;
            _methodBodyElementFinder = methodBodyElementFinder;
            _parameters = parameters;
        }

        public string FindName()
        {
            return _name;
        }

        public IEnumerable<IParameterElementFinder> FindParameters()
        {
            return _parameters.Select(param => new FakeParameterElementFinder(param));
        }

        public IMethodBodyElementFinder FindBody()
        {
            return _methodBodyElementFinder;
        }

        public int GetLineNumber()
        {
            return Line;
        }

        public int GetEndLineNumber()
        {
            return Line;
        }

        public bool IsStatic()
        {
            return false;
        }
    }
}