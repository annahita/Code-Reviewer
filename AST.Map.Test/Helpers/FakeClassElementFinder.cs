using System.Collections.Generic;
using Python.Antlr.ElementFinders;

namespace Python.AST.Map.Test.Helpers
{
    public class FakeClassElementFinder : IClassElementFinder
    {
        private const int Line = 1;
        private readonly IEnumerable<IAssignStatementElementFinder> _assignStatementElementFinders;
        private readonly IEnumerable<IMethodElementFinder> _constructorsElementFinders;
        private readonly IEnumerable<IMethodElementFinder> _methodElementFinders;
        private readonly string _name;
        private readonly IEnumerable<string> _superClasses;

        public FakeClassElementFinder(string name, IEnumerable<string> superClasses)
        {
            _name = name;
            _superClasses = superClasses;
            _assignStatementElementFinders = new List<IAssignStatementElementFinder>();
            _methodElementFinders = new List<IMethodElementFinder>();
            _constructorsElementFinders = new List<IMethodElementFinder>();
        }

        public FakeClassElementFinder(string name,
            IEnumerable<IAssignStatementElementFinder> assignStatementElementFinders)
        {
            _name = name;
            _assignStatementElementFinders = assignStatementElementFinders;
            _superClasses = new List<string>();
            _methodElementFinders = new List<IMethodElementFinder>();
            _constructorsElementFinders = new List<IMethodElementFinder>();
        }

        public FakeClassElementFinder(string name, IEnumerable<IMethodElementFinder> methodElementFinders)
        {
            _name = name;
            _methodElementFinders = methodElementFinders;
            _superClasses = new List<string>();
            _assignStatementElementFinders = new List<IAssignStatementElementFinder>();
            _constructorsElementFinders = new List<IMethodElementFinder>();
        }

        public FakeClassElementFinder(string name, IMethodElementFinder constructorsElementFinders)
        {
            _name = name;
            _constructorsElementFinders = new[] {constructorsElementFinders};
            _superClasses = new List<string>();
            _assignStatementElementFinders = new List<IAssignStatementElementFinder>();
            _methodElementFinders = new List<IMethodElementFinder>();
        }

        public string FindName()
        {
            return _name;
        }

        public IEnumerable<string> FindSuperClasses()
        {
            return _superClasses;
        }

        public IEnumerable<IAssignStatementElementFinder> FindAssignStatements()
        {
            return _assignStatementElementFinders;
        }

        public IEnumerable<IMethodElementFinder> FindConstructors()
        {
            return _constructorsElementFinders;
        }

        public IEnumerable<IMethodElementFinder> FindInstanceMethods()
        {
            return _methodElementFinders;
        }

        public IEnumerable<IMethodElementFinder> FindStaticMethods()
        {
            return _methodElementFinders;
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