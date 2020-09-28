using System.Collections.Generic;

namespace OOP_Model
{
    public interface IClassDeclaration : IEntityBase
    {
        string Name { get; }
        IEnumerable<string> SuperClasses { get; }
        IEnumerable<IMethodBase> Methods { get; }
        IEnumerable<IMethodBase> Constructors { get; }
        IEnumerable<IFieldDeclaration> Fields { get; }
    }
}