using OOP_Model;

namespace Python.Model
{
    public class FieldDeclaration : EntityBase, IFieldDeclaration
    {
        public FieldDeclaration(int lineNumber) : base(lineNumber)
        {
        }

        public bool IsInstanceField { get; private set; }

        public IIdentifier Identifier { get; private set; }
        public string DataType { get; private set; }
        public bool IsStatic { get; private set; }

        public void SetIdentifier(Identifier identifier)
        {
            Identifier = identifier;
        }

        public void SetVariableType(string type)
        {
            DataType = type;
        }

        public void SetAsStaticField()
        {
            IsStatic = true;
        }

        public void SetAsInstanceField()
        {
            IsInstanceField = true;
        }
    }
}