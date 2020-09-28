using System.Collections.Generic;

namespace OOP_Model
{
    public interface IEmbeddedStatement : IEntityBase
    {
        IEnumerable<IEmbeddedStatement> InternalEmbeddedStatement { get; }
        int GetLinesOfCodeCount();
        int CountNestedStructures();
    }
}