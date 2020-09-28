using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Tree;
using Python.Antlr.Extension;
using Python.Antlr.Path;
using Python.Antlr.Properties;

namespace Python.Antlr.ElementFinders
{
    public interface IClassElementFinder : IElementFinder
    {
        string FindName();
        IEnumerable<string> FindSuperClasses();
        IEnumerable<IAssignStatementElementFinder> FindAssignStatements();
        IEnumerable<IMethodElementFinder> FindConstructors();
        IEnumerable<IMethodElementFinder> FindInstanceMethods();
        IEnumerable<IMethodElementFinder> FindStaticMethods();
    }

    public class ClassElementFinder : ElementFinderBase, IClassElementFinder
    {
        private readonly string _constructorName = Resources.python_Cunstructor_keyword;
        private readonly string _staticMethodDecorator = Resources.python_StaticMethodDecorator;

        public ClassElementFinder(IParseTree tree, PythonParser parser) : base(tree, parser)
        {
        }

        public string FindName()
        {
            var nameTree = FindInPath(PathConfig.ClassName).FirstOrDefault();
            return nameTree.GetText();
        }

        public IEnumerable<string> FindSuperClasses()
        {
            var argTrees = FindInPath(PathConfig.ClassArgs);
            return argTrees.Select(argTree => argTree.GetText());
        }

        public IEnumerable<IAssignStatementElementFinder> FindAssignStatements()
        {
            var assignStatementTrees = FilterByAssignStatement(FindClassBodySmallStatements());
            return assignStatementTrees.Select(assignStatementTree =>
                new AssignStatementElementFinder(assignStatementTree, _parser));
        }

        public IEnumerable<IMethodElementFinder> FindConstructors()
        {
            var constructorTrees = FindConstructorTrees();
            return constructorTrees.Select(constructorTree => new MethodElementFinder(constructorTree, _parser));
        }

        public IEnumerable<IMethodElementFinder> FindInstanceMethods()
        {
            var methodTrees = FindMethodTrees();
            var instanceTrees = methodTrees.Where(methodTree => !IsStatic(methodTree));
            return instanceTrees.Select(instanceTree => new MethodElementFinder(instanceTree, _parser));
        }

        public IEnumerable<IMethodElementFinder> FindStaticMethods()
        {
            var methodTrees = FindMethodTrees();
            var staticTrees = methodTrees.Where(IsStatic);
            return staticTrees.Select(staticTree => new MethodElementFinder(staticTree, _parser));
        }

        private IEnumerable<IParseTree> FindClassBodySmallStatements()
        {
            return FindInPath(PathConfig.ClassBodySmallStatements);
        }

        private IEnumerable<IParseTree> FilterByAssignStatement(IEnumerable<IParseTree> trees)
        {
            return trees.FindByContextLabel<PythonParser.Assign_stmtContext>();
        }

        private IEnumerable<IParseTree> FindConstructorTrees()
        {
            var functionNameTrees = FindInPath(PathConfig.FunctionName);
            var constructorNameTrees = functionNameTrees.Where(a => a.GetText().Equals(_constructorName));
            return constructorNameTrees.Select(a => a.Parent);
        }

        private IEnumerable<IParseTree> FindMethodTrees()
        {
            var functionNameTrees = FindInPath(PathConfig.FunctionName);
            var constructorNameTrees = functionNameTrees.Where(a => !a.GetText().Equals(_constructorName));
            return constructorNameTrees.Select(a => a.Parent);
        }

        private bool IsStatic(IParseTree tree)
        {
            var decorators = FindInPath(tree.Parent, PathConfig.DecoratorName);
            return decorators.Any(a => a.GetText().Equals(_staticMethodDecorator));
        }
    }
}