using OOP_Model;
using Python.Antlr.ElementFinders;
using Python.AST.Map.RelationResolver;
using Python.Model;

namespace Python.AST.Map.Expressions.Variables.Strategies
{
    internal interface IVariableMappingStrategy
    {
        IAstMapper<IExpression> GetMapper(IIdentifierElementFinder elementFinder, Identifier identifier);
        void SetParent(IParentReferenceResolver parent);
    }
}