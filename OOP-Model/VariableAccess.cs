namespace OOP_Model
{
    public interface IVariableAccess : IEntityBase, IExpression
    {
        IIdentifier Identifier { get; }
        bool IsMemberOfParentClass();
        bool IsLocalVariable();
        bool IsMethodParameter();
        bool IsStatic();
        bool IsUndefinedAccess();
    }
}