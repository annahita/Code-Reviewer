using OOP_Model;

namespace Python.Model
{
    public abstract class EntityBase : IEntityBase
    {
        protected EntityBase(int lineNumber)
        {
            LineNumber = lineNumber;
        }

        public int LineNumber { get; }

        public object GetThis()
        {
            return this;
        }
    }
}