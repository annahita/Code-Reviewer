using Analyzer;

namespace CleanCode.Analyzers
{
    public static class QualityRules
    {
        public static readonly QualityRule PronounceableName = new QualityRule(
            "Name is not pronounceable");

        public static readonly QualityRule SearchableName = new QualityRule(
            "Name is too short and not searchable");

        public static readonly QualityRule NounForClassName = new QualityRule(
            "Class names should be noun");

        public static readonly QualityRule VerbForMethodName = new QualityRule(
            "Method names should be Verb");

        public static readonly QualityRule ClassCohesion = new QualityRule(
            "Class is not cohesive enough");

        public static readonly QualityRule SideEffect = new QualityRule(
            "Modifying non local variables might causes side effects");

        public static readonly QualityRule MethodParametersCount = new QualityRule(
            "Too many methods parameters might causes confusion");

        public static readonly QualityRule MethodSize = new QualityRule(
            "Long Methods are hard to read and probably doing more than one thing");

        public static readonly QualityRule NestedStructure = new QualityRule(
            "MultiLevel Nested embedded structure");

        public static readonly QualityRule EmbeddedStatementsBlockSize = new QualityRule(
            "Too many lines of code in block embedded structures might affects readability");
    }
}