using CleanCode.Metrics;
using CleanCode.Strategies.LCOM;
using CleanCode.Strategies.Name;
using CleanCode.Strategies.SideEffects;
using CleanCode.Utils.Name;
using CleanCode.Utils.WordNet;
using CleanCode.Utils.WordNet.searchableFile;

namespace CleanCode.Test.TestHelpers
{
    public static class TestBootStrapped
    {
        public static INamingConvention GetInstanceOfNamingConvention()
        {
            var wordLookUp = new LookUp(GetInstanceOfIStreamReader());
            var nameSplitter = new Splitter();
            var metric = GetInstanceOfIMetric();
            return new NamingConvention(wordLookUp, nameSplitter, metric);
        }

        public static IMetric GetInstanceOfIMetric()
        {
            return new Metric();
        }

        public static ICohesion GetInstanceOfICohesion()
        {
            return new Lcom4(GetInstanceOfIMetric());
        }

        public static ISearchableFile GetInstanceOfIStreamReader()
        {
            return new SearchableFile();
        }

        public static ISideEffect GetInstanceOfISideEffect()
        {
            return new ModifiedStateSideEffect();
        }
    }
}