using System.Collections.Generic;
using Analyzer;
using CleanCode.Strategies.Name;
using CleanCode.Utils.WordNet;
using OOP_Model;

namespace CleanCode.Analyzers
{
    public class ClassName : ICodeAnalyzer<IClassDeclaration>
    {
        private const PartsOfSpeech PartOfSpeechForClass = PartsOfSpeech.Noun;
        private readonly INamingConvention _namingConvention;
        private List<AnalyzeError> _analyzeErrors;

        public ClassName(INamingConvention namingConvention)
        {
            _namingConvention = namingConvention;
        }

        public void Analyze(IClassDeclaration objectDeclaration)
        {
            _analyzeErrors = new List<AnalyzeError>();
            var className = objectDeclaration.Name;
            var lineNumber = objectDeclaration.LineNumber;
            CheckIsNamePronounceable(className, lineNumber);
            CheckIsNameSearchable(className, lineNumber);
            CheckIsClassNameNoun(className, lineNumber);
        }

        public IEnumerable<AnalyzeError> GetResult()
        {
            return _analyzeErrors;
        }

        private void CheckIsNamePronounceable(string className, int lineNumber)
        {
            if (!_namingConvention.IsPronounceable(className))
                _analyzeErrors.Add(new AnalyzeError(QualityRules.PronounceableName, className, lineNumber));
        }

        private void CheckIsNameSearchable(string className, int lineNumber)
        {
            if (!_namingConvention.IsSearchable(className))
                _analyzeErrors.Add(new AnalyzeError(QualityRules.SearchableName, className, lineNumber));
        }

        private void CheckIsClassNameNoun(string className, int lineNumber)
        {
            if (!_namingConvention.HasWritePartOfSpeech(className, PartOfSpeechForClass))
                _analyzeErrors.Add(new AnalyzeError(QualityRules.NounForClassName, className, lineNumber));
        }
    }
}