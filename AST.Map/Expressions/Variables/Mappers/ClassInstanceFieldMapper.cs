using Python.Antlr.ElementFinders;
using Python.Model;

namespace Python.AST.Map.Expressions.Variables.Mappers
{
    internal sealed class ClassInstanceFieldMapper : FieldMapperBase
    {
        internal ClassInstanceFieldMapper(IIdentifierElementFinder elementFinder, Identifier identifier) : base(
            elementFinder, identifier)
        {
        }

        protected override void SetFieldType()
        {
            Field.SetAsInstanceField();
        }
    }
}