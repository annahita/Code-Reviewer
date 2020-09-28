using System.Collections.Generic;
using System.Linq;
using Python.Antlr.ElementFinders;
using Python.Model;

namespace Python.AST.Map.Methods
{
    internal abstract class MethodMapperBase : IAstMapper<MethodDeclaration>
    {
        private readonly ClassDeclaration _parent;
        protected IMethodElementFinder ElementFinder { get; }
        protected MethodDeclaration Method { get; }

        protected MethodMapperBase(ClassDeclaration parent, IMethodElementFinder elementFinder)
        {
            _parent = parent;
            ElementFinder = elementFinder;
            Method = new MethodDeclaration(ElementFinder.GetLineNumber());
            MapElements();
        }

        public MethodDeclaration GetMappedItem()
        {
            return Method;
        }

        private void MapElements()
        {
            MapSpacialElements();
            Method.SetParentClass(_parent);
            Method.SetName(MapName());
            Method.SetParameters(MapMethodParameters());
            Method.SetMethodBody(MapMethodBlock());
        }

        protected abstract void MapSpacialElements();


        private string MapName()
        {
            return ElementFinder.FindName();
        }

        private IEnumerable<ParameterDeclaration> MapMethodParameters()
        {
            var parameters = FindMethodParameters();
            return parameters.Select(param => new ParameterMapper(param).GetMappedItem());
        }

        private MethodBody MapMethodBlock()
        {
            var body = ElementFinder.FindBody();
            var mapper = new MethodBodyMapper(body, Method);
            return mapper.GetMappedItem();
        }

        protected abstract IEnumerable<IParameterElementFinder> FindMethodParameters();
    }
}