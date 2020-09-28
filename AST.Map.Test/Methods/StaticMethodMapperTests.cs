using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.AST.Map.Methods;
using Python.AST.Map.Test.Helpers;
using Python.AST.Map.Test.Helpers.Models;
using Python.Model;

namespace Python.AST.Map.Test.Methods
{
    [TestClass]
    public class StaticMethodMapperTests
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
            var methodElementsFinder = new FakeStaticMethodElementFinder(NewMethodName);
            var mapper = new StaticMethodMapper(_parentClass, methodElementsFinder);

            var mappedMethod = mapper.GetMappedItem();
            var mappedMethodName = mappedMethod.Name;

            Assert.AreEqual(NewMethodName, mappedMethodName);
            Assert.IsTrue(mappedMethod.IsStatic);
            Assert.IsFalse(mappedMethod.HasInstancePointer());
        }

        [TestMethod]
        public void StaticMethod_WithParameters_HasNotInstancePointer()
        {
            var methodElementsFinder =
                new FakeStaticMethodElementFinder(NewMethodName, new[] {InstancePointer, ParameterName});
            var mapper = new StaticMethodMapper(_parentClass, methodElementsFinder);

            var mappedMethod = mapper.GetMappedItem();
            var mappedParameters = mappedMethod.Parameters.Select(a => a.Name).ToList();

            CollectionAssert.Contains(mappedParameters, ParameterName);
            CollectionAssert.Contains(mappedParameters, InstancePointer);

            Assert.IsFalse(mappedMethod.HasInstancePointer());
        }
    }
}