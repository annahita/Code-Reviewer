namespace CleanCode.Metrics
{
    public interface IMetric
    {
        short MaximumSizeOfEmbeddedBlock { get; }
        short MaximumNumberOfMethodParameters { get; }
        short MaximumSizeOfMethod { get; }
        short AcceptableLcom4 { get; }
        short MaximumLevelOfNestedStructure { get; }
        short MinimumLengthOfName { get; }
    }
}