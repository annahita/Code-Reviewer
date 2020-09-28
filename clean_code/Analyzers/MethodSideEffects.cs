using System.Collections.Generic;
using Analyzer;
using CleanCode.Strategies.SideEffects;
using OOP_Model;

namespace CleanCode.Analyzers
{
    public class MethodSideEffects : ICodeAnalyzer<IMethodBase>
    {
        private readonly QualityRule _rule;
        private readonly ISideEffect _sideEffect;
        private List<AnalyzeError> _analyzeErrors;

        public MethodSideEffects(ISideEffect sideEffect)
        {
            _sideEffect = sideEffect;
            _rule = QualityRules.SideEffect;
        }

        public void Analyze(IMethodBase objectDeclaration)
        {
            _analyzeErrors = new List<AnalyzeError>();
            _sideEffect.CheckForSideEffects(objectDeclaration);
            if (_sideEffect.HasSideEffects())
                _analyzeErrors.Add(new AnalyzeError(_rule, objectDeclaration.Name, objectDeclaration.LineNumber));
        }

        public IEnumerable<AnalyzeError> GetResult()
        {
            return _analyzeErrors;
        }
    }
}