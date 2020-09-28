using System.Collections.Generic;
using System.Linq;
using Python.Antlr;
using Python.Antlr.ElementFinders;
using Python.AST.Map.Classes;
using Python.Model;

namespace Python.AST.Map
{
    public class RootMapper : IRootMapper
    {
        private readonly IParserBuilder _parserBuilder;

        private RootContainer _rootNode;

        public RootMapper(IParserBuilder parserBuilder)
        {
            _parserBuilder = parserBuilder;
        }

        #region Public Methods

        private ClassDeclaration MapClassMembers(IClassElementFinder classElementFinder)
        {
            var mapper = new ClassMapper(classElementFinder);
            return mapper.GetMappedItem();
        }

        public RootContainer GetMappedItem()
        {
            return _rootNode;
        }

        #endregion

        #region private Methods

        public void MapElements(string code)
        {
            var finder = GetRootElementFinder(code);
            _rootNode = new RootContainer(finder.GetLineNumber());
            _rootNode.SetClasses(GetClassDeclarations(finder));
        }

        private IRootElementFinder GetRootElementFinder(string code)
        {
            _parserBuilder.Build(code);
            var finder = _parserBuilder.GetParseElementFinder();
            return finder;
        }

        private IEnumerable<ClassDeclaration> GetClassDeclarations(IRootElementFinder elementFinder)
        {
            return elementFinder.FindClasses().Select(MapClassMembers).ToList();
        }

        #endregion
    }
}