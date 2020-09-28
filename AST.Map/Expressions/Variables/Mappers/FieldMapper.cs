using Python.Antlr.ElementFinders;
using Python.Model;

namespace Python.AST.Map.Expressions.Variables.Mappers
{
    internal abstract class FieldMapperBase : IAstMapper<FieldDeclaration>
    {
        private readonly IIdentifierElementFinder _elementFinder;
        private readonly Identifier _identifier;
        protected FieldDeclaration Field { get; private set; }

        protected FieldMapperBase(IIdentifierElementFinder elementFinder, Identifier identifier)
        {
            _identifier = identifier;
            _elementFinder = elementFinder;
            MapElements();
        }

        public FieldDeclaration GetMappedItem()
        {
            return Field;
        }

        private void MapElements()
        {
            Field = new FieldDeclaration(_elementFinder.GetLineNumber());
            Field.SetIdentifier(_identifier);
            SetFieldType();
        }

        protected abstract void SetFieldType();
    }
}