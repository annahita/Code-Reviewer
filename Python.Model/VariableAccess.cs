using OOP_Model;

namespace Python.Model
{
    public sealed class VariableAccess : EntityBase, IVariableAccess
    {
        public VariableAccess(int lineNumber) : base(lineNumber)
        {
        }

        public IExpression AccessedExpression { get; private set; }

        public IIdentifier Identifier { get; private set; }

        public bool IsMemberOfParentClass()
        {
            return AccessedExpression is FieldDeclaration;
        }

        public bool IsLocalVariable()
        {
            return AccessedExpression is LocalVariableDeclaration;
        }

        public bool IsMethodParameter()
        {
            return AccessedExpression is ParameterDeclaration;
        }

        public bool IsStatic()
        {
            return AccessedExpression is FieldDeclaration declaration && declaration.IsStatic;
        }

        public bool IsUndefinedAccess()
        {
            return AccessedExpression is NullExpression;
        }

        public void SetIdentifier(Identifier identifier)
        {
            Identifier = identifier;
        }

        public void SetAccessedExpression(IExpression expression)
        {
            AccessedExpression = expression;
        }
    }
}