namespace OOP_Model
{
    public interface IFieldDeclaration : IEntityBase, IExpression
    {
        IIdentifier Identifier { get; }
        string DataType { get; }
        bool IsStatic { get; }
    }
}