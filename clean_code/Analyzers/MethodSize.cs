using System.Collections.Generic;
using Analyzer;
using CleanCode.Metrics;
using OOP_Model;

namespace CleanCode.Analyzers
{
    public class MethodSize : ICodeAnalyzer<IMethodBase>
    {
        private readonly int _maxLinesInMethod;
        private readonly QualityRule _rule;
        private List<AnalyzeError> _analyzeErrors;

        public MethodSize(IMetric metrics)
        {
            _maxLinesInMethod = metrics.MaximumSizeOfMethod;
            _rule = QualityRules.MethodSize;
        }

        public void Analyze(IMethodBase objectDeclaration)
        {
            _analyzeErrors = new List<AnalyzeError>();
            if (HasTooManyLines(objectDeclaration))
                _analyzeErrors.Add(new AnalyzeError(_rule, objectDeclaration.Name, objectDeclaration.LineNumber));
        }

        public IEnumerable<AnalyzeError> GetResult()
        {
            return _analyzeErrors;
        }

        private bool HasTooManyLines(IMethodBase method)
        {
            return method.MethodBody.GetLinesOfCodeCount() > _maxLinesInMethod;
        }
    }
}