using System.Collections.Generic;
using Analyzer;
using CleanCode.Strategies.Name;
using CleanCode.Utils.WordNet;
using OOP_Model;

namespace CleanCode.Analyzers
{
    public class MethodName : ICodeAnalyzer<IMethodBase>
    {
        private const PartsOfSpeech PartOfSpeechForMethod = PartsOfSpeech.Verb;
        private readonly INamingConvention _namingConvention;

        private List<AnalyzeError> _analyzeErrors;

        public MethodName(INamingConvention namingConvention)
        {
            _namingConvention = namingConvention;
        }

        public void Analyze(IMethodBase objectDeclaration)
        {
            _analyzeErrors = new List<AnalyzeError>();
            var methodName = objectDeclaration.Name;
            var lineNumber = objectDeclaration.LineNumber;
            CheckIsNamePronounceable(methodName, lineNumber);
            CheckIsNameSearchable(methodName, lineNumber);
            CheckIsMethodNameVerb(methodName, lineNumber);
        }

        public IEnumerable<AnalyzeError> GetResult()
        {
            return _analyzeErrors;
        }

        private void CheckIsNamePronounceable(string methodName, int lineNumber)
        {
            if (!_namingConvention.IsPronounceable(methodName))
                _analyzeErrors.Add(new AnalyzeError(QualityRules.PronounceableName, methodName, lineNumber));
        }

        private void CheckIsNameSearchable(string methodName, int lineNumber)
        {
            if (!_namingConvention.IsSearchable(methodName))
                _analyzeErrors.Add(new AnalyzeError(QualityRules.SearchableName, methodName, lineNumber));
        }

        private void CheckIsMethodNameVerb(string methodName, int lineNumber)
        {
            if (!_namingConvention.HasWritePartOfSpeech(methodName, PartOfSpeechForMethod))
                _analyzeErrors.Add(new AnalyzeError(QualityRules.VerbForMethodName, methodName, lineNumber));
        }
    }
}