using System.Collections.Generic;
using OOP_Model;
using Python.Antlr.ElementFinders;
using Python.AST.Map.Expressions.Identifiers;
using Python.AST.Map.RelationResolver;
using Python.Model;

namespace Python.AST.Map.Expressions
{
    internal sealed class MethodInvocationMapper : IAstMapper<MethodInvocation>
    {
        private readonly IMethodInvocationElementFinder _elementFinder;
        private readonly IParentReferenceResolver _parent;
        private MethodInvocation _invokedMethod;

        internal MethodInvocationMapper(IMethodInvocationElementFinder elementFinder, IParentReferenceResolver parent)
        {
            _elementFinder = elementFinder;
            _parent = parent;
            MapElements();
        }

        public MethodInvocation GetMappedItem()
        {
            return _invokedMethod;
        }

        private void MapElements()
        {
            _invokedMethod = new MethodInvocation(_elementFinder.GetLineNumber());
            var methodIdentifier = MapIdentifier();
            _invokedMethod.SetMethodIdentifier(methodIdentifier);
            _invokedMethod.SetArguments(MapArguments());
            _invokedMethod.SetMemberOfParentClassState(IsMemberOfParentClass(methodIdentifier));
        }

        private Identifier MapIdentifier()
        {
            var identifierMapper = new IdentifierMapper(_elementFinder.FindIdentifier(), _parent);
            return identifierMapper.GetMappedItem();
        }

        private IEnumerable<IExpression> MapArguments()
        {
            var argumentMapper = new MethodArgumentMapper(_elementFinder.FindArgumentCollection(), _parent);
            return argumentMapper.GetMappedItem();
        }

        private bool IsMemberOfParentClass(Identifier methodIdentifier)
        {
            return methodIdentifier.HasInstancePointer();
        }
    }
}