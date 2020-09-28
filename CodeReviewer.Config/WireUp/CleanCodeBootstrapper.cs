using Analyzer;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CleanCode.Analyzers;
using CleanCode.Metrics;
using CleanCode.Strategies.LCOM;
using CleanCode.Strategies.Name;
using CleanCode.Strategies.SideEffects;
using CleanCode.Utils.Name;
using CleanCode.Utils.WordNet;
using CleanCode.Utils.WordNet.searchableFile;

namespace CodeReviewer.Config.WireUp
{
    public static class CleanCodeBootstrapper
    {
        public static void WireUp(IWindsorContainer container)
        {
            WireUpUtils(container);
            WireUpStrategies(container);
            WireUpCodeAnalyzers(container);
        }

        private static void WireUpStrategies(IWindsorContainer container)
        {
            container.Register(Component.For<IMetric>().ImplementedBy<Metric>().LifestyleTransient());
            container.Register(Component.For<ICohesion>().ImplementedBy<Lcom4>().LifestyleTransient());
            container.Register(Component.For<ISideEffect>().ImplementedBy<ModifiedStateSideEffect>()
                .LifestyleTransient());
            container.Register(
                Component.For<INamingConvention>().ImplementedBy<NamingConvention>().LifestyleTransient());
        }

        private static void WireUpUtils(IWindsorContainer container)
        {
            container.Register(Component.For<ISearchableFile>().ImplementedBy<SearchableFile>().LifestyleTransient());
            container.Register(Component.For<IWordLookUp>().ImplementedBy<LookUp>().LifestyleTransient());
            container.Register(Component.For<INameSplitter>().ImplementedBy<Splitter>().LifestyleTransient());
        }

        private static void WireUpCodeAnalyzers(IWindsorContainer container)
        {
            container.Register(Component.For<IRootAnalyzer>().ImplementedBy<RootAnalyzer>().LifestyleTransient());
            container.Register(Component.For<IAnalyzer>().ImplementedBy<ClassCohesion>().LifestyleTransient());
            container.Register(Component.For<IAnalyzer>().ImplementedBy<ClassName>().LifestyleTransient());
            container.Register(Component.For<IAnalyzer>().ImplementedBy<EmbeddedStatementAnalyzer>()
                .LifestyleTransient());
            container.Register(Component.For<IAnalyzer>().ImplementedBy<MethodName>().LifestyleTransient());
            container.Register(Component.For<IAnalyzer>().ImplementedBy<MethodSideEffects>().LifestyleTransient());
            container.Register(Component.For<IAnalyzer>().ImplementedBy<MethodSize>().LifestyleTransient());
            container.Register(Component.For<IAnalyzer>().ImplementedBy<ParametersCount>().LifestyleTransient());
        }
    }
}