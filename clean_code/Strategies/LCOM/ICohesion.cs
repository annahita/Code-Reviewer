using OOP_Model;

namespace CleanCode.Strategies.LCOM
{
    public interface ICohesion
    {
        void AnalyzeCohesion(IClassDeclaration classDeclaration);
        bool IsCohesive();
    }
}