using CleanCode.Strategies.LCOM;
using CleanCode.Test.TestHelpers.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOP_Model;

namespace CleanCode.Test.Strategies.LCOM
{
    [TestClass]
    public class LCOM4GraphTest
    {
        private readonly string[] _classFieldsIdentifier = {"field1", "Field2"};

        [TestMethod]
        public void IFMethods_AccessedSameClassFields_ShouldBeConnected()
        {
            var method1 = GetMethodWithAccessingToClassFieldsInBlock("Method1", _classFieldsIdentifier);
            var method2 = GetMethodWithAccessingToClassFieldsInBlock("Method2", new[] {_classFieldsIdentifier[0]});

            var lcomGraph = new Lcom4Graph(new[] {method1, method2});
            var actualConnectedComponentsCount = lcomGraph.CountConnectedComponents();

            var expectedConnectedComponentCounts = 1;

            Assert.AreEqual(expectedConnectedComponentCounts, actualConnectedComponentsCount);
        }

        [TestMethod]
        public void IFMethods_ModifiedSameClassFields_ShouldBeConnected()
        {
            var method1 = GetMethodWithModifiedClassFieldsInBlock("Method1", _classFieldsIdentifier);
            var method2 = GetMethodWithModifiedClassFieldsInBlock("Method2", new[] {_classFieldsIdentifier[0]});

            var lcomGraph = new Lcom4Graph(new[] {method1, method2});
            var actualConnectedComponentsCount = lcomGraph.CountConnectedComponents();

            var expectedConnectedComponentCounts = 1;

            Assert.AreEqual(expectedConnectedComponentCounts, actualConnectedComponentsCount);
        }

        [TestMethod]
        public void IFMethods_UsedDifferentClassFields_ShouldNotBeConnected()
        {
            var method1 = GetMethodWithAccessingToClassFieldsInBlock("Method1", new[] {_classFieldsIdentifier[0]});
            var method2 = GetMethodWithAccessingToClassFieldsInBlock("Method2", new[] {_classFieldsIdentifier[1]});

            var lcomGraph = new Lcom4Graph(new[] {method1, method2});
            var actualConnectedComponentsCount = lcomGraph.CountConnectedComponents();

            var expectedConnectedComponentCounts = 2;

            Assert.AreEqual(expectedConnectedComponentCounts, actualConnectedComponentsCount);
        }

        [TestMethod]
        public void IFMethods_CallsOtherMethod_ShouldBeConnected()
        {
            var method1 = MockMethod.Method("Method1", MockBlock.GeneralMethodBody().Object);
            var method2Body = MockBlock.GetMethodBodyThatInvokedInternalMethods(new[] {method1.Name});
            var method2 = MockMethod.Method("Method2", method2Body);

            var lcomGraph = new Lcom4Graph(new[] {method1, method2});
            var actualConnectedComponentsCount = lcomGraph.CountConnectedComponents();

            var expectedConnectedComponentCounts = 1;

            Assert.AreEqual(expectedConnectedComponentCounts, actualConnectedComponentsCount);
        }

        private IMethodBase GetMethodWithAccessingToClassFieldsInBlock(string methodName, string[] fieldsIdentifiers)
        {
            var methodBody = MockBlock.GetMethodBodyThatAccessedClassFields(fieldsIdentifiers);
            return MockMethod.Method(methodName, methodBody);
        }

        private IMethodBase GetMethodWithModifiedClassFieldsInBlock(string methodName, string[] fieldsIdentifiers)
        {
            var methodBody = MockBlock.GetMethodBodyThatModifiedClassFields(fieldsIdentifiers);
            return MockMethod.Method(methodName, methodBody);
        }
    }
}