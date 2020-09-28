using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Tree;
using Python.Antlr.Path;

namespace Python.Antlr.ElementFinders
{
    public interface IRootElementFinder : IElementFinder
    {
        IEnumerable<IClassElementFinder> FindClasses();
    }

    public class RootElementFinder : ElementFinderBase, IRootElementFinder
    {
        public RootElementFinder(IParseTree tree, PythonParser parser) : base(tree, parser)
        {
        }

        public IEnumerable<IClassElementFinder> FindClasses()
        {
            var classTrees = FindInPath(PathConfig.ClassDef);
            return classTrees.Select(classTree => new ClassElementFinder(classTree, _parser));
        }
    }
}