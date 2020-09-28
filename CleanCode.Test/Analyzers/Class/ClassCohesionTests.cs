using System.Linq;
using CleanCode.Analyzers;
using CleanCode.Test.TestHelpers;
using CleanCode.Test.TestHelpers.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOP_Model;

namespace CleanCode.Test.Analyzers.Class
{
    [TestClass]
    public class ClassCohesionTests
    {
        [TestMethod]
        public void ClassWithMethods_NotUsingAnyOfClassFields_HasCohesionError()
        {
            var cohesionRule = QualityRules.ClassCohesion;

            var method1 = MockMethod.EmptyMethod("Method1");
            var method2 = MockMethod.EmptyMethod("Method2");
            var classFields = new[] {"field1", "field2"};

            var classDef = MockClass.GetClass(classFields, new[] {method1, method2});
            var cohesionAnalyzer = new ClassCohesion(TestBootStrapped.GetInstanceOfICohesion());
            cohesionAnalyzer.Analyze(classDef);
            var analyzeResult = cohesionAnalyzer.GetResult();

            var isCohesive = !analyzeResult.Any(a => a.ViolatedRule.Equals(cohesionRule));

            Assert.IsFalse(isCohesive);
        }

        [TestMethod]
        public void ClassWithMethods_UsedCommonClassFields_HasNotCohesionError()
        {
            var classFields = new[] {"field1", "field2"};

            var cohesionRule = QualityRules.ClassCohesion;

            var method1 = GetMethodWithAccessingToClassFieldsInBlock("Method1", classFields);
            var method2 = GetMethodWithAccessingToClassFieldsInBlock("Method2", new[] {classFields[0]});

            var classDef = MockClass.GetClass(classFields, new[] {method1, method2});
            var cohesionAnalyzer = new ClassCohesion(TestBootStrapped.GetInstanceOfICohesion());
            cohesionAnalyzer.Analyze(classDef);
            var analyzeResult = cohesionAnalyzer.GetResult();

            var isCohesive = !analyzeResult.Any(a => a.ViolatedRule.Equals(cohesionRule));

            Assert.IsTrue(isCohesive);
        }

        [TestMethod]
        public void ClassWithMethods_HaveNotUsedCommonClassFields_HasCohesionError()
        {
            var classFields = new[] {"field1", "field2"};

            var cohesionRule = QualityRules.ClassCohesion;

            var method1 = GetMethodWithAccessingToClassFieldsInBlock("Method1", new[] {classFields[0]});
            var method2 = GetMethodWithAccessingToClassFieldsInBlock("Method2", new[] {classFields[1]});

            var classDef = MockClass.GetClass(classFields, new[] {method1, method2});
            var cohesionAnalyzer = new ClassCohesion(TestBootStrapped.GetInstanceOfICohesion());
            cohesionAnalyzer.Analyze(classDef);
            var analyzeResult = cohesionAnalyzer.GetResult();

            var isCohesive = !analyzeResult.Any(a => a.ViolatedRule.Equals(cohesionRule));

            Assert.IsFalse(isCohesive);
        }

        [TestMethod]
        public void ClassWithMethods_HaveNotUsedCommonClassFields_ButConnectedByCallingMethods_HasNotCohesionError()
        {
            var classFields = new[] {"field1", "field2"};

            var cohesionRule = QualityRules.ClassCohesion;

            var method1 = MockMethod.Method("Method1", MockBlock.GeneralMethodBody().Object);
            var method2Body = MockBlock.GetMethodBodyThatInvokedInternalMethods(new[] {method1.Name});
            var method2 = MockMethod.Method("Method2", method2Body);


            var classDef = MockClass.GetClass(classFields, new[] {method1, method2});
            var cohesionAnalyzer = new ClassCohesion(TestBootStrapped.GetInstanceOfICohesion());
            cohesionAnalyzer.Analyze(classDef);
            var analyzeResult = cohesionAnalyzer.GetResult();

            var isCohesive = !analyzeResult.Any(a => a.ViolatedRule.Equals(cohesionRule));

            Assert.IsTrue(isCohesive);
        }

        private IMethodBase GetMethodWithAccessingToClassFieldsInBlock(string methodName, string[] fieldsIdentifiers)
        {
            var methodBody = MockBlock.GetMethodBodyThatAccessedClassFields(fieldsIdentifiers);
            return MockMethod.Method(methodName, methodBody);
        }
    }
}