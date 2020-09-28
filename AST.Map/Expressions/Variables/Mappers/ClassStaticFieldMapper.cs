using Python.Antlr.ElementFinders;
using Python.Model;

namespace Python.AST.Map.Expressions.Variables.Mappers
{
    internal sealed class ClassStaticFieldMapper : FieldMapperBase
    {
        internal ClassStaticFieldMapper(IIdentifierElementFinder elementFinder, Identifier identifier) : base(
            elementFinder, identifier)
        {
        }

        protected override void SetFieldType()
        {
            Field.SetAsStaticField();
        }
    }
}