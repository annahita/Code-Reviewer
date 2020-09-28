using System.Linq;
using CleanCode.Analyzers;
using CleanCode.Test.TestHelpers;
using CleanCode.Test.TestHelpers.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanCode.Test.Analyzers.Method
{
    [TestClass]
    public class MethodSideEffectsTests
    {
        [TestMethod]
        public void Method_JustModifiedLocalVariable_HasNotAnalyzerError()
        {
            var sideEffectRule = QualityRules.SideEffect;

            var localVariable = "variable1";
            var methodName = "newMethod";
            var body = MockBlock.GetMethodBodyThatJustModifiedLocalVariable(localVariable);
            var method = MockMethod.Method(methodName, body);

            var sideEffectAnalyzer = new MethodSideEffects(TestBootStrapped.GetInstanceOfISideEffect());
            sideEffectAnalyzer.Analyze(method);
            var analyzeResult = sideEffectAnalyzer.GetResult();

            var hasSideEffect = analyzeResult.Any(a => a.ViolatedRule.Equals(sideEffectRule));

            Assert.IsFalse(hasSideEffect);
        }

        [TestMethod]
        public void Method_JustModifiedParentClassFields_HasNotAnalyzerError()
        {
            var sideEffectRule = QualityRules.SideEffect;

            var methodParameter = "param1";
            var body = MockBlock.GetMethodBodyThatJustModifiedClassInstanceFields(methodParameter);
            var method = MockMethod.DefaultMethodWithBody(body);

            var sideEffectAnalyzer = new MethodSideEffects(TestBootStrapped.GetInstanceOfISideEffect());
            sideEffectAnalyzer.Analyze(method);
            var analyzeResult = sideEffectAnalyzer.GetResult();

            var hasSideEffect = analyzeResult.Any(a => a.ViolatedRule.Equals(sideEffectRule));

            Assert.IsFalse(hasSideEffect);
        }

        [TestMethod]
        public void Method_ModifiedParameters_HasAnalyzerError()
        {
            var sideEffectRule = QualityRules.SideEffect;

            var classFields = "field1";
            var body = MockBlock.GetMethodBodyThatModifiedMethodParameters(classFields);
            var method = MockMethod.DefaultMethodWithBody(body);

            var sideEffectAnalyzer = new MethodSideEffects(TestBootStrapped.GetInstanceOfISideEffect());
            sideEffectAnalyzer.Analyze(method);
            var analyzeResult = sideEffectAnalyzer.GetResult();

            var hasSideEffect = analyzeResult.Any(a => a.ViolatedRule.Equals(sideEffectRule));

            Assert.IsTrue(hasSideEffect);
        }

        [TestMethod]
        public void Method_ModifiedStaticFields_HasAnalyzerError()
        {
            var sideEffectRule = QualityRules.SideEffect;

            var classStaticField = "staticFieldOfClass";
            var body = MockBlock.GetMethodBodyThatModifiedStaticFields(classStaticField);
            var method = MockMethod.DefaultMethodWithBody(body);

            var sideEffectAnalyzer = new MethodSideEffects(TestBootStrapped.GetInstanceOfISideEffect());
            sideEffectAnalyzer.Analyze(method);
            var analyzeResult = sideEffectAnalyzer.GetResult();

            var hasSideEffect = analyzeResult.Any(a => a.ViolatedRule.Equals(sideEffectRule));

            Assert.IsTrue(hasSideEffect);
        }
    }
}