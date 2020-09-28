using System.Collections.Generic;
using System.Linq;
using Python.Antlr.ElementFinders;

namespace Python.AST.Map.Test.Helpers
{
    public class FakeStaticMethodElementFinder : IMethodElementFinder
    {
        private const int Line = 1;
        private readonly string _name;
        private readonly IEnumerable<string> _parameters;

        public FakeStaticMethodElementFinder(string name, IEnumerable<string> parameters)
        {
            _name = name;
            _parameters = parameters;
        }

        public FakeStaticMethodElementFinder(string name)
        {
            _name = name;
            _parameters = null;
        }

        public string FindName()
        {
            return _name;
        }

        public IEnumerable<IParameterElementFinder> FindParameters()
        {
            if (_parameters != null) return _parameters.Select(param => new FakeParameterElementFinder(param));

            return new List<IParameterElementFinder>();
        }

        public IMethodBodyElementFinder FindBody()
        {
            return new FakeIMethodBodyElementFinder();
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
            return true;
        }
    }
}