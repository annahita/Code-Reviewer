namespace Analyzer
{
    public class QualityRule
    {
        public QualityRule(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}