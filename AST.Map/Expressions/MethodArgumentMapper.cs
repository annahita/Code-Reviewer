using System.Collections.Generic;
using System.Linq;
using OOP_Model;
using Python.Antlr.ElementFinders;
using Python.AST.Map.Expressions.Variables;
using Python.AST.Map.Expressions.Variables.Strategies;
using Python.AST.Map.RelationResolver;

namespace Python.AST.Map.Expressions
{
    internal sealed class MethodArgumentMapper : IAstMapper<IEnumerable<IExpression>>
    {
        private readonly IArgumentElementFinder _elementFinder;
        private readonly IParentReferenceResolver _parent;
        private List<IExpression> _expressions;

        internal MethodArgumentMapper(IArgumentElementFinder elementFinder, IParentReferenceResolver parent)
        {
            _elementFinder = elementFinder;
            _parent = parent;
            MapElements();
        }

        public IEnumerable<IExpression> GetMappedItem()
        {
            return _expressions;
        }

        private void MapElements()
        {
            _expressions = new List<IExpression>();
            _expressions.AddRange(MapVariableArguments());
            _expressions.AddRange(MapMethodInvocationArguments());
        }

        private IEnumerable<IExpression> MapVariableArguments()
        {
            var args = _elementFinder.FindArgumentsWithVariableType();
            var mappingStrategy = new AccessedVariableStrategy();
            return args.Select(arg => new VariableMapperBridge(arg, _parent, mappingStrategy).GetMappedItem());
        }

        private IEnumerable<IExpression> MapMethodInvocationArguments()
        {
            var args = _elementFinder.FindArgumentsWithMethodInvocationType();
            return args.Select(arg => new MethodInvocationMapper(arg, _parent).GetMappedItem());
        }
    }
}