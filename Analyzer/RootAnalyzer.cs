using System.Collections.Generic;
using System.Linq;
using OOP_Model;

namespace Analyzer
{
    public class RootAnalyzer : IRootAnalyzer
    {
        private readonly IEnumerable<ICodeAnalyzer<IClassDeclaration>> _classAnalyzers;
        private readonly IEnumerable<ICodeAnalyzer<IMethodBase>> _methodAnalyzers;
        private List<AnalyzeError> _analyzeErrors;

        public RootAnalyzer(IList<IAnalyzer> analyzers)
        {
            _methodAnalyzers = analyzers.Filter<ICodeAnalyzer<IMethodBase>>();
            _classAnalyzers = analyzers.Filter<ICodeAnalyzer<IClassDeclaration>>();
        }

        public void Analyze(IRootContainer root)
        {
            _analyzeErrors = new List<AnalyzeError>();
            foreach (var classDeclaration in root.ClassDeclarations)
            {
                RunAnalyzers(classDeclaration);
                foreach (var method in classDeclaration.Methods) RunAnalyzers(method);
            }
        }

        public IEnumerable<AnalyzeError> GetAnalyzedResults()
        {
            return _analyzeErrors;
        }

        private void RunAnalyzers(IMethodBase method)
        {
            foreach (var item in _methodAnalyzers.Where(a => a is ICodeAnalyzer<IMethodBase>))
            {
                item.Analyze(method);
                _analyzeErrors.AddRange(item.GetResult());
            }
        }

        private void RunAnalyzers(IClassDeclaration classDeclaration)
        {
            foreach (var item in _classAnalyzers.Where(a => a is ICodeAnalyzer<IClassDeclaration>))
            {
                item.Analyze(classDeclaration);
                _analyzeErrors.AddRange(item.GetResult());
            }
        }
    }
}