using OOP_Model;

namespace CleanCode.Strategies.SideEffects
{
    public interface ISideEffect
    {
        void CheckForSideEffects(IMethodBase method);
        bool HasSideEffects();
    }
}