using OOP_Model;

namespace Python.Model

{
    public class LocalVariableDeclaration : EntityBase, ILocalVariableDeclaration
    {
        public LocalVariableDeclaration(int lineNumber) : base(lineNumber)
        {
        }

        public IIdentifier Identifier { get; private set; }
        public string DataType { get; private set; }


        public void SetIdentifier(Identifier identifier)
        {
            Identifier = identifier;
        }

        public void SetVariableType(string type)
        {
            DataType = type;
        }
    }
}