using OOP_Model;

namespace Python.Model
{
    public class NullExpression : IExpression
    {
        public object GetThis()
        {
            return this;
        }
    }
}