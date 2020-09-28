namespace Analyzer
{
    public class AnalyzeError
    {
        public AnalyzeError(QualityRule rule, string objectIdentifier, int lineNumber)
        {
            LineNumber = lineNumber;
            ViolatedRule = rule;
            Identifier = objectIdentifier;
        }

        public string Identifier { get; }
        public int LineNumber { get; }
        public QualityRule ViolatedRule { get; }
    }
}