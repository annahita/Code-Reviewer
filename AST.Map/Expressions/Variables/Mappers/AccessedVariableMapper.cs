using OOP_Model;
using Python.Antlr.ElementFinders;
using Python.Model;

namespace Python.AST.Map.Expressions.Variables.Mappers
{
    internal sealed class AccessedVariableMapper : IAstMapper<VariableAccess>
    {
        private readonly IExpression _accessedExpression;
        private readonly IIdentifierElementFinder _elementFinder;
        private readonly Identifier _identifier;
        private VariableAccess _variableAccess;

        internal AccessedVariableMapper(IIdentifierElementFinder elementFinder, Identifier identifier,
            IExpression accessedExpression)
        {
            _elementFinder = elementFinder;
            _identifier = identifier;
            _accessedExpression = accessedExpression;
            MapElements();
        }

        public VariableAccess GetMappedItem()
        {
            return _variableAccess;
        }

        private void MapElements()
        {
            _variableAccess = new VariableAccess(_elementFinder.GetLineNumber());
            _variableAccess.SetIdentifier(_identifier);
            _variableAccess.SetAccessedExpression(_accessedExpression);
        }
    }
}