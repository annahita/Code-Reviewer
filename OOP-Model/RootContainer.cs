using System.Collections.Generic;

namespace OOP_Model
{
    public interface IRootContainer : IEntityBase
    {
        IEnumerable<IClassDeclaration> ClassDeclarations { get; }
    }
}