namespace OOP_Model
{
    public interface ILocalVariableDeclaration : IEntityBase, IExpression
    {
        IIdentifier Identifier { get; }
        string DataType { get; }
    }
}