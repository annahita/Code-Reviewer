using Python.Antlr.ElementFinders;
using Python.Model;

namespace Python.AST.Map.Expressions.Variables.Mappers
{
    internal sealed class LocalVariableMapper : IAstMapper<LocalVariableDeclaration>
    {
        private readonly IIdentifierElementFinder _elementFinder;
        private readonly Identifier _identifier;
        private LocalVariableDeclaration _localVariable;

        internal LocalVariableMapper(IIdentifierElementFinder elementFinder, Identifier identifier)
        {
            _identifier = identifier;
            _elementFinder = elementFinder;
            MapElements();
        }

        public LocalVariableDeclaration GetMappedItem()
        {
            return _localVariable;
        }

        private void MapElements()
        {
            _localVariable = new LocalVariableDeclaration(_elementFinder.GetLineNumber());
            _localVariable.SetIdentifier(_identifier);
        }
    }
}