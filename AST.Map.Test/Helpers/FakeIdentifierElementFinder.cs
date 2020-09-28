using System.Collections.Generic;
using Python.Antlr.ElementFinders;

namespace Python.AST.Map.Test.Helpers
{
    public class FakeIdentifierElementFinder : IIdentifierElementFinder
    {
        private readonly IEnumerable<string> _identifier;
        private readonly int _line = 1;

        public FakeIdentifierElementFinder(IEnumerable<string> identifier, int line = 1)
        {
            _identifier = identifier;
            _line = line;
        }

        public FakeIdentifierElementFinder()
        {
            _identifier = new[] {"default"};
        }

        public int GetLineNumber()
        {
            return _line;
        }

        public int GetEndLineNumber()
        {
            return _line;
        }

        public IEnumerable<string> FindIdentifier()
        {
            return _identifier;
        }
    }
}