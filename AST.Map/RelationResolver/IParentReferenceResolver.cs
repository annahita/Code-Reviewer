using OOP_Model;
using Python.Model;

namespace Python.AST.Map.RelationResolver
{
    internal interface IParentReferenceResolver
    {
        IExpression FindMember(Identifier id);
        ExpressionType ResolveType(Identifier id);
        bool IsInstancePointer(string id);
    }
}