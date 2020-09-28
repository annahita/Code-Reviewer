using System.Collections.Generic;
using Analyzer;
using CodeReviewer.Config.Resolvers;
using OOP_Model;
using Python.AST.Map;
using Python.Model;

namespace MainApp
{
    public static class CleanCodeAnalyzer
    {
        public static IEnumerable<AnalyzeError> Analyze(string code)
        {
            var result = BuildModelOfCode(code);
            return AnalyzeCode(result);
        }

        private static RootContainer BuildModelOfCode(string code)
        {
            var mapper = DependencyResolver.Current.Resolve<IRootMapper>();
            mapper.MapElements(code);
            var result = mapper.GetMappedItem();
            return result;
        }

        private static IEnumerable<AnalyzeError> AnalyzeCode(IRootContainer root)
        {
            var analyzer = DependencyResolver.Current.Resolve<IRootAnalyzer>();
            analyzer.Analyze(root);
            return analyzer.GetAnalyzedResults();
        }
    }
}