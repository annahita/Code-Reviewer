using Python.Antlr.ElementFinders;
using Python.Model;

namespace Python.AST.Map.Methods
{
    internal sealed class ParameterMapper : IAstMapper<ParameterDeclaration>
    {
        private readonly IParameterElementFinder _elementFinder;
        private ParameterDeclaration _parameter;


        internal ParameterMapper(IParameterElementFinder elementFinder)
        {
            _elementFinder = elementFinder;

            MapElements();
        }

        public ParameterDeclaration GetMappedItem()
        {
            return _parameter;
        }

        private void MapElements()
        {
            _parameter = new ParameterDeclaration(_elementFinder.GetLineNumber());
            _parameter.SetName(_elementFinder.FindParameterName());
        }
    }
}