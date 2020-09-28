using OOP_Model;
using Python.Antlr.ElementFinders;
using Python.AST.Map.Expressions.Variables.Mappers;
using Python.AST.Map.RelationResolver;
using Python.Model;

namespace Python.AST.Map.Expressions.Variables.Strategies
{
    internal class AccessedVariableStrategy : IVariableMappingStrategy
    {
        private IParentReferenceResolver Parent { get; set; }

        public void SetParent(IParentReferenceResolver parent)
        {
            Parent = parent;
        }

        public IAstMapper<IExpression> GetMapper(IIdentifierElementFinder elementFinder, Identifier identifier)
        {
            var accessedMember = Parent.FindMember(identifier);
            return new AccessedVariableMapper(elementFinder, identifier, accessedMember);
        }
    }
}