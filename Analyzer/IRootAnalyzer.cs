using System.Collections.Generic;
using OOP_Model;

namespace Analyzer
{
    public interface IRootAnalyzer
    {
        void Analyze(IRootContainer root);
        IEnumerable<AnalyzeError> GetAnalyzedResults();
    }
}