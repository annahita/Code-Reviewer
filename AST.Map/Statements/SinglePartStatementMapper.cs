using System.Collections.Generic;
using System.Linq;
using OOP_Model;
using Python.Antlr.ElementFinders;
using Python.AST.Map.Expressions;
using Python.AST.Map.Expressions.Variables;
using Python.AST.Map.Expressions.Variables.Strategies;
using Python.AST.Map.RelationResolver;
using Python.Model;

namespace Python.AST.Map.Statements
{
    internal sealed class SinglePartStatementMapper : IAstMapper<SinglePartStatement>
    {
        private readonly ISingleStatementElementFinder _elementFinder;

        private readonly IParentReferenceResolver _parent;
        private SinglePartStatement _statement;

        internal SinglePartStatementMapper(ISingleStatementElementFinder elementFinder, IParentReferenceResolver parent)
        {
            _elementFinder = elementFinder;

            _parent = parent;
            MapElements();
        }

        public SinglePartStatement GetMappedItem()
        {
            return _statement;
        }

        private void MapElements()
        {
            _statement = new SinglePartStatement(_elementFinder.GetLineNumber());
            var expressions = MapVariables().Concat(MapInvokedMethods());
            _statement.SetExpressions(expressions);
        }

        private IEnumerable<IExpression> MapVariables()
        {
            var variables = _elementFinder.FindVariables();
            var mappingStrategy = new AccessedVariableStrategy();
            return variables.Select(variable =>
                new VariableMapperBridge(variable, _parent, mappingStrategy).GetMappedItem());
        }

        private IEnumerable<IExpression> MapInvokedMethods()
        {
            var invokedMethods = _elementFinder.FindMethodInvocations();
            return invokedMethods.Select(invokedMethod =>
                new MethodInvocationMapper(invokedMethod, _parent).GetMappedItem());
        }
    }
}