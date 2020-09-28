using System.Collections.Generic;
using System.Linq;
using Analyzer;
using CleanCode.Metrics;
using OOP_Model;

namespace CleanCode.Analyzers
{
    public class ParametersCount : ICodeAnalyzer<IMethodBase>
    {
        private readonly int _maxParameters;
        private readonly QualityRule _rule;
        private List<AnalyzeError> _analyzeErrors;

        public ParametersCount(IMetric metrics)
        {
            _maxParameters = metrics.MaximumNumberOfMethodParameters;
            _rule = QualityRules.MethodParametersCount;
        }

        public void Analyze(IMethodBase objectDeclaration)
        {
            _analyzeErrors = new List<AnalyzeError>();
            if (HasTooManyParameters(objectDeclaration))
                _analyzeErrors.Add(new AnalyzeError(_rule, objectDeclaration.Name, objectDeclaration.LineNumber));
        }

        public IEnumerable<AnalyzeError> GetResult()
        {
            return _analyzeErrors;
        }

        private bool HasTooManyParameters(IMethodBase method)
        {
            return method.Parameters.Count() > _maxParameters;
        }
    }
}