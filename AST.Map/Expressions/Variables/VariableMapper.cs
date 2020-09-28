using OOP_Model;
using Python.Antlr.ElementFinders;
using Python.AST.Map.Expressions.Identifiers;
using Python.AST.Map.Expressions.Variables.Strategies;
using Python.AST.Map.RelationResolver;
using Python.Model;

namespace Python.AST.Map.Expressions.Variables
{
    internal class VariableMapperBridge : IAstMapper<IExpression>
    {
        private readonly IIdentifierElementFinder _elementFinder;
        private readonly IVariableMappingStrategy _mappingStrategy;
        private readonly IParentReferenceResolver _parent;
        private IExpression _expression;

        internal VariableMapperBridge(IIdentifierElementFinder elementFinder, IParentReferenceResolver parent,
            IVariableMappingStrategy mappingStrategy)

        {
            _elementFinder = elementFinder;
            _parent = parent;
            _mappingStrategy = mappingStrategy;
            _mappingStrategy.SetParent(_parent);
            MapElements();
        }


        public IExpression GetMappedItem()
        {
            return _expression;
        }

        private void MapElements()
        {
            _expression = MapVariable();
        }

        private IExpression MapVariable()
        {
            var identifier = MapIdentifier();
            var mapper = _mappingStrategy.GetMapper(_elementFinder, identifier);
            return mapper.GetMappedItem();
        }

        private Identifier MapIdentifier()
        {
            var mapper = new IdentifierMapper(_elementFinder, _parent);
            return mapper.GetMappedItem();
        }
    }
}