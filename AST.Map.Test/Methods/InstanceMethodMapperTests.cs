using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.AST.Map.Methods;
using Python.AST.Map.Test.Helpers;
using Python.AST.Map.Test.Helpers.Models;
using Python.Model;

namespace Python.AST.Map.Test.Methods
{
    [TestClass]
    public class InstanceMethodMapperTests
    {
        private const string NewMethodName = "newMethod";
        private const string ParameterName = "ParameterName";
        private const string InstancePointer = "InstancePointer";
        private ClassDeclaration _parentClass;

        [TestInitialize]
        public void TestInit()
        {
            _parentClass = new BuildDefaultModel().GetClass();
        }

        [TestMethod]
        public void TestMapping_ParameterLess_Method()
        {
            var methodElementsFinder = new FakeInstanceMethodElementFinder(NewMethodName, new[] {InstancePointer});
            var mapper = new InstanceMethodMapper(_parentClass, methodElementsFinder);

            var mappedMethod = mapper.GetMappedItem();
            var mappedMethodName = mappedMethod.Name;

            Assert.AreEqual(NewMethodName, mappedMethodName);
            Assert.IsFalse(mappedMethod.IsStatic);
            Assert.IsTrue(mappedMethod.HasInstancePointer());
        }

        [TestMethod]
        public void TestMapping_Method_WithParameters()
        {
            var methodElementsFinder =
                new FakeInstanceMethodElementFinder(NewMethodName, new[] {InstancePointer, ParameterName});
            var mapper = new InstanceMethodMapper(_parentClass, methodElementsFinder);

            var mappedMethod = mapper.GetMappedItem();
            var mappedParameters = mappedMethod.Parameters.Select(a => a.Name).ToList();
            var mappedInstancePointer = mappedMethod.InstancePointer.Name;

            CollectionAssert.Contains(mappedParameters, ParameterName);
            CollectionAssert.DoesNotContain(mappedParameters, InstancePointer);
            Assert.AreEqual(InstancePointer, mappedInstancePointer);
        }
    }
}