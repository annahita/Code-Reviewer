using Python.Antlr.ElementFinders;

namespace Python.AST.Map.Test.Helpers
{
    public class FakeParameterElementFinder : IParameterElementFinder
    {
        private const int Line = 1;
        private readonly string _name;

        public FakeParameterElementFinder(string name)
        {
            _name = name;
        }

        public string FindParameterName()
        {
            return _name;
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