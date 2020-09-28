using System.Collections.Generic;

namespace OOP_Model
{
    public interface IMethodBase : IEntityBase
    {
        string Name { get; }
        IEnumerable<IParameterDeclaration> Parameters { get; }
        IMethodBody MethodBody { get; }
    }
}