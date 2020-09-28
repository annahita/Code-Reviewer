namespace OOP_Model
{
    public interface IVariableModification : IEntityBase, IExpression
    {
        IIdentifier Identifier { get; }
        bool IsMemberOfParentClass();
        bool IsLocalVariable();
        bool IsMethodParameter();
        bool IsStatic();
    }
}