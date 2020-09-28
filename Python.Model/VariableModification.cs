using OOP_Model;

namespace Python.Model
{
    public class VariableModification : EntityBase, IVariableModification
    {
        public VariableModification(int lineNumber) : base(lineNumber)
        {
        }

        public IExpression ModifiedExpression { get; private set; }

        public IIdentifier Identifier { get; private set; }

        public bool IsMemberOfParentClass()
        {
            return ModifiedExpression is FieldDeclaration;
        }

        public bool IsLocalVariable()
        {
            return ModifiedExpression is LocalVariableDeclaration;
        }

        public bool IsMethodParameter()
        {
            return ModifiedExpression is ParameterDeclaration;
        }

        public bool IsStatic()
        {
            return ModifiedExpression is FieldDeclaration declaration && declaration.IsStatic;
        }

        public void SetIdentifier(Identifier identifier)
        {
            Identifier = identifier;
        }

        public void SetModifiedExpression(IExpression expression)
        {
            ModifiedExpression = expression;
        }
    }
}