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
    internal sealed class AssignStatementMapper : IAstMapper<AssignStatement>
    {
        private readonly IAssignStatementElementFinder _elementFinder;
        private readonly IParentReferenceResolver _parent;
        private AssignStatement _statement;

        internal AssignStatementMapper(IAssignStatementElementFinder elementFinder, IParentReferenceResolver parent)
        {
            _elementFinder = elementFinder;
            _parent = parent;
            MapElements();
        }

        public AssignStatement GetMappedItem()
        {
            return _statement;
        }

        private void MapElements()
        {
            _statement = new AssignStatement(_elementFinder.GetLineNumber());

            _statement.SetLeftExpressions(MapLeft());
            _statement.SetRightExpressions(MapRight());
        }

        private IEnumerable<IExpression> MapLeft()
        {
            return MapVariablesInLeftPart().Concat(MapInvokedMethods());
        }

        private IEnumerable<IExpression> MapRight()
        {
            return MapVariablesInRightPart().Concat(MapInvokedMethods());
        }

        private IEnumerable<IExpression> MapVariablesInLeftPart()
        {
            var variables = _elementFinder.FindVariablesInLeftPart();
            var mappingStrategy = new AlteredVariableStrategy();
            return variables.Select(variable =>
                new VariableMapperBridge(variable, _parent, mappingStrategy).GetMappedItem());
        }

        private IEnumerable<IExpression> MapVariablesInRightPart()
        {
            var variables = _elementFinder.FindVariablesInRightPart();
            var mappingStrategy = new AccessedVariableStrategy();
            return variables.Select(variable =>
                new VariableMapperBridge(variable, _parent, mappingStrategy).GetMappedItem());
        }

        private IEnumerable<IExpression> MapInvokedMethods()
        {
            var methodInvocations = _elementFinder.FindMethodInvocations();
            return methodInvocations.Select(methodInvocation =>
                new MethodInvocationMapper(methodInvocation, _parent).GetMappedItem());
        }
    }
}