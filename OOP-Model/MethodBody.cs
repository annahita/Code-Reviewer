using System.Collections.Generic;

namespace OOP_Model
{
    public interface IMethodBody : IEntityBase
    {
        IEnumerable<ISimpleStatement> SimpleStatements { get; }
        IEnumerable<IEmbeddedStatement> EmbeddedStatements { get; }
        IEnumerable<ILocalVariableDeclaration> GetDeclaredLocalVariables();
        IEnumerable<IVariableAccess> GetAccessedFields();
        IEnumerable<IVariableModification> GetModifiedFields();
        IEnumerable<IMethodInvocation> GetInvokedMethods();

        int GetEmptyLinesCount();
        int GetLinesOfCodeCount();
    }
}