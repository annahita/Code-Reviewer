using System.Linq;
using CleanCode.Analyzers;
using CleanCode.Metrics;
using CleanCode.Test.TestHelpers;
using CleanCode.Test.TestHelpers.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanCode.Test.Analyzers.Method.Parameters
{
    [TestClass]
    public class ParametersCountTests
    {
        [TestMethod]
        public void Method_WithParametersCount_GreaterThanLimit_HasAnalyzerError()
        {
            var methodParameterRule = QualityRules.MethodParametersCount;
            var maximumParameters = new Metric().MaximumNumberOfMethodParameters;

            const string methodName = "newMethod";
            var methodParameters = MockParameter.ParameterDeclarations(maximumParameters + 1);
            var method = MockMethod.Method(methodName, methodParameters);

            var parameterAnalyzer = new ParametersCount(TestBootStrapped.GetInstanceOfIMetric());
            parameterAnalyzer.Analyze(method);
            var analyzeResult = parameterAnalyzer.GetResult();

            var hasAcceptableNumberOfParameters = !analyzeResult.Any(a => a.ViolatedRule.Equals(methodParameterRule));

            Assert.IsFalse(hasAcceptableNumberOfParameters);
        }

        [TestMethod]
        public void MethodWith_LimitParameters_HasNotAnalyzerError()
        {
            var methodParameterRule = QualityRules.MethodParametersCount;
            var maximumParameters = new Metric().MaximumNumberOfMethodParameters;

            var methodName = "newMethod";
            var methodParameters = MockParameter.ParameterDeclarations(maximumParameters - 1);
            var method = MockMethod.Method(methodName, methodParameters);

            var parameterAnalyzer = new ParametersCount(TestBootStrapped.GetInstanceOfIMetric());
            parameterAnalyzer.Analyze(method);
            var analyzeResult = parameterAnalyzer.GetResult();

            var hasAcceptableNumberOfParameters = !analyzeResult.Any(a => a.ViolatedRule.Equals(methodParameterRule));

            Assert.IsTrue(hasAcceptableNumberOfParameters);
        }
    }
}