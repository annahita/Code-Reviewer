using CleanCode.Metrics;
using OOP_Model;

namespace CleanCode.Strategies.LCOM
{
    public class Lcom4 : ICohesion
    {
        private readonly short _acceptableLcom4;
        private bool _isCohesive;

        public Lcom4(IMetric metric)
        {
            _acceptableLcom4 = metric.AcceptableLcom4;
        }

        public void AnalyzeCohesion(IClassDeclaration classDeclaration)
        {
            var graph = new Lcom4Graph(classDeclaration.Methods);
            var count = graph.CountConnectedComponents();
            if (count == _acceptableLcom4) _isCohesive = true;
        }

        public bool IsCohesive()
        {
            return _isCohesive;
        }
    }
}