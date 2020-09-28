using System.Collections.Generic;
using Analyzer;
using CleanCode.Strategies.LCOM;
using OOP_Model;

namespace CleanCode.Analyzers
{
    public class ClassCohesion : ICodeAnalyzer<IClassDeclaration>
    {
        private readonly ICohesion _cohesion;
        private readonly QualityRule _rule;
        private List<AnalyzeError> _analyzeErrors;

        public ClassCohesion(ICohesion cohesion)
        {
            _cohesion = cohesion;
            _rule = QualityRules.ClassCohesion;
        }

        public void Analyze(IClassDeclaration objectDeclaration)
        {
            _analyzeErrors = new List<AnalyzeError>();
            _cohesion.AnalyzeCohesion(objectDeclaration);
            if (!_cohesion.IsCohesive())
                _analyzeErrors.Add(new AnalyzeError(_rule, objectDeclaration.Name, objectDeclaration.LineNumber));
        }

        public IEnumerable<AnalyzeError> GetResult()
        {
            return _analyzeErrors;
        }
    }
}