using System.Collections.Generic;

namespace Analyzer
{
    public interface ICodeAnalyzer<in T> : IAnalyzer
    {
        void Analyze(T objectDeclaration);
        IEnumerable<AnalyzeError> GetResult();
    }
}