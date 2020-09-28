using System.Linq;
using CleanCode.Analyzers;
using CleanCode.Metrics;
using CleanCode.Test.TestHelpers;
using CleanCode.Test.TestHelpers.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanCode.Test.Analyzers.Method
{
    [TestClass]
    public class MethodSizeTests
    {
        [TestMethod]
        public void MethodWith_GreaterThanLimitBlockSize_HasAnalyzerError()
        {
            var methodSizeRule = QualityRules.MethodSize;
            var maximumLines = new Metric().MaximumSizeOfMethod;

            var body = MockBlock.GeneralMethodBodyWithBlockSize(maximumLines + 1);
            var method = MockMethod.DefaultMethodWithBody(body.Object);
            var methodSizeAnalyzer = new MethodSize(TestBootStrapped.GetInstanceOfIMetric());
            methodSizeAnalyzer.Analyze(method);
            var analyzeResult = methodSizeAnalyzer.GetResult();

            var hasAcceptableSize = !analyzeResult.Any(a => a.ViolatedRule.Equals(methodSizeRule));

            Assert.IsFalse(hasAcceptableSize);
        }

        [TestMethod]
        public void MethodWith_SmallBlockSize_HasNotAnalyzerError()
        {
            var methodSizeRule = QualityRules.MethodSize;
            var maximumLines = new Metric().MaximumSizeOfMethod;

            var body = MockBlock.GeneralMethodBodyWithBlockSize(maximumLines - 1);
            var method = MockMethod.DefaultMethodWithBody(body.Object);
            var methodSizeAnalyzer = new MethodSize(TestBootStrapped.GetInstanceOfIMetric());
            methodSizeAnalyzer.Analyze(method);
            var analyzeResult = methodSizeAnalyzer.GetResult();

            var hasAcceptableSize = !analyzeResult.Any(a => a.ViolatedRule.Equals(methodSizeRule));

            Assert.IsTrue(hasAcceptableSize);
        }
    }
}