using System.Collections.Generic;
using Analyzer;
using CleanCode.Metrics;
using OOP_Model;

namespace CleanCode.Analyzers
{
    public class EmbeddedStatementAnalyzer : ICodeAnalyzer<IMethodBase>
    {
        private readonly int _maxLinesInBlock;
        private readonly int _maxNestedLevel;
        private List<AnalyzeError> _analyzeErrors;

        public EmbeddedStatementAnalyzer(IMetric metrics)
        {
            _maxNestedLevel = metrics.MaximumLevelOfNestedStructure;
            _maxLinesInBlock = metrics.MaximumSizeOfEmbeddedBlock;
        }

        public void Analyze(IMethodBase objectDeclaration)
        {
            _analyzeErrors = new List<AnalyzeError>();
            foreach (var embedded in objectDeclaration.MethodBody.EmbeddedStatements)
            {
                CheckSizeOfCodeBlock(embedded, objectDeclaration.Name);
                CheckLevelOfNestedStructures(embedded, objectDeclaration.Name);
            }
        }

        public IEnumerable<AnalyzeError> GetResult()
        {
            return _analyzeErrors;
        }

        private void CheckLevelOfNestedStructures(IEmbeddedStatement embedded, string methodName)
        {
            if (HasTooManyNestedLevels(embedded))
                _analyzeErrors.Add(new AnalyzeError(QualityRules.NestedStructure, methodName, embedded.LineNumber));
        }

        private void CheckSizeOfCodeBlock(IEmbeddedStatement embedded, string methodName)
        {
            if (HasTooManyLinesInBlock(embedded))
                _analyzeErrors.Add(new AnalyzeError(QualityRules.EmbeddedStatementsBlockSize, methodName,
                    embedded.LineNumber));
        }

        private bool HasTooManyLinesInBlock(IEmbeddedStatement structure)
        {
            var exceededMaximum = structure.GetLinesOfCodeCount() > _maxLinesInBlock;
            return exceededMaximum;
        }

        private bool HasTooManyNestedLevels(IEmbeddedStatement structure)
        {
            return structure.CountNestedStructures() > _maxNestedLevel;
        }
    }
}