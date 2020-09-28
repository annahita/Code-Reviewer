namespace OOP_Model
{
    public interface IParameterDeclaration : IEntityBase, IExpression
    {
        string Name { get; }
        string DataType { get; }
    }
}