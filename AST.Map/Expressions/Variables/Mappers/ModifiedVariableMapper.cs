using OOP_Model;
using Python.Antlr.ElementFinders;
using Python.Model;

namespace Python.AST.Map.Expressions.Variables.Mappers
{
    internal sealed class ModifiedVariableMapper : IAstMapper<VariableModification>
    {
        private readonly IIdentifierElementFinder _elementFinder;
        private readonly Identifier _identifier;
        private readonly IExpression _modifiedExpression;
        private VariableModification _modifiedVariable;

        internal ModifiedVariableMapper(IIdentifierElementFinder elementFinder, Identifier identifier,
            IExpression modifiedExpression)
        {
            _identifier = identifier;
            _modifiedExpression = modifiedExpression;
            _elementFinder = elementFinder;
            MapElements();
        }

        public VariableModification GetMappedItem()
        {
            return _modifiedVariable;
        }

        private void MapElements()
        {
            _modifiedVariable = new VariableModification(_elementFinder.GetLineNumber());
            _modifiedVariable.SetIdentifier(_identifier);
            _modifiedVariable.SetModifiedExpression(_modifiedExpression);
        }
    }
}