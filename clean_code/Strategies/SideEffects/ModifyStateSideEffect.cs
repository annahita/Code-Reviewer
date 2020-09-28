using System.Collections.Generic;
using OOP_Model;

namespace CleanCode.Strategies.SideEffects
{
    public class ModifiedStateSideEffect : ISideEffect
    {
        private IMethodBase _method;
        private ICollection<IVariableModification> _unSafeModifiedVariables;

        public void CheckForSideEffects(IMethodBase method)
        {
            _method = method;
            FindUnsafeModifications();
        }

        public bool HasSideEffects()
        {
            var hasUnsafeModification = _unSafeModifiedVariables.Count > 0;
            return hasUnsafeModification;
        }

        private void FindUnsafeModifications()
        {
            _unSafeModifiedVariables = new List<IVariableModification>();
            foreach (var expr in _method.MethodBody.GetModifiedFields())
                if (!IsSafeModification(expr))
                    _unSafeModifiedVariables.Add(expr);
        }

        private bool IsSafeModification(IVariableModification expression)
        {
            return expression.IsMemberOfParentClass() || expression.IsLocalVariable();
        }
    }
}