using System.Linq;
using CleanCode.Analyzers;
using CleanCode.Metrics;
using CleanCode.Test.TestHelpers;
using CleanCode.Test.TestHelpers.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanCode.Test.Analyzers.EmbeddedStatementTests
{
    [TestClass]
    public class EmbeddedStatementTests
    {
        [TestMethod]
        public void DeepNested_EmbeddedStatement_HasAnalyzerError()
        {
            var nestedStructureRules = QualityRules.NestedStructure;
            var maximumNestedStructures = new Metric().MaximumLevelOfNestedStructure;

            var nestedStructure =
                MockIEmbeddedStatement.MultiLevelNestedIEmbeddedStatements(maximumNestedStructures + 1);
            var body = MockBlock.GetMethodBodyWithEmbeddedStatement(new[] {nestedStructure});
            var method = MockMethod.DefaultMethodWithBody(body);

            var nestedStructureRAnalyzer = new EmbeddedStatementAnalyzer(TestBootStrapped.GetInstanceOfIMetric());
            nestedStructureRAnalyzer.Analyze(method);
            var analyzeResult = nestedStructureRAnalyzer.GetResult();

            var hasError = analyzeResult.Any(a => a.ViolatedRule.Equals(nestedStructureRules));

            Assert.IsTrue(hasError);
        }

        [TestMethod]
        public void EmbeddedStatement_WithBigBlock_HasAnalyzerError()
        {
            var blockSizeRule = QualityRules.EmbeddedStatementsBlockSize;
            var maximumSizeOfEmbeddedBlock = new Metric().MaximumSizeOfEmbeddedBlock;

            var embeddedStatement = MockIEmbeddedStatement.GeneralIEmbeddedStatement(maximumSizeOfEmbeddedBlock + 1);
            var body = MockBlock.GetMethodBodyWithEmbeddedStatement(new[] {embeddedStatement});
            var method = MockMethod.DefaultMethodWithBody(body);

            var nestedStructureRAnalyzer = new EmbeddedStatementAnalyzer(TestBootStrapped.GetInstanceOfIMetric());
            nestedStructureRAnalyzer.Analyze(method);
            var analyzeResult = nestedStructureRAnalyzer.GetResult();

            var hasError = analyzeResult.Any(a => a.ViolatedRule.Equals(blockSizeRule));

            Assert.IsTrue(hasError);
        }
    }
}