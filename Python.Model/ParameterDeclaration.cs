using OOP_Model;

namespace Python.Model
{
    public class ParameterDeclaration : EntityBase, IParameterDeclaration
    {
        public ParameterDeclaration(int lineNumber) : base(lineNumber)
        {
        }

        public string Name { get; private set; }
        public string DataType { get; private set; }

        public void SetName(string name)
        {
            Name = name;
        }
    }
}