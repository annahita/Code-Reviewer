using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.Antlr.ElementFinders;
using Python.AST.Map.Expressions;
using Python.AST.Map.RelationResolver;
using Python.AST.Map.Test.Helpers;
using Python.AST.Map.Test.Helpers.Models;

namespace Python.AST.Map.Test.Expressions
{
    [TestClass]
    public class MethodInvocationMapperTests
    {
        private const string MethodName = "invokedMethod";
        private string _declaredFieldId;
        private string _instancePointer;
        private IParentReferenceResolver _parentReferenceResolver;

        [TestInitialize]
        public void TestInit()
        {
            var parentMethod = new BuildDefaultModel().GetMethod();
            var declaredField = parentMethod.ParentClass.Fields.FirstOrDefault();
            _declaredFieldId = declaredField.Identifier.GetField();
            _instancePointer = parentMethod.InstancePointer.Name;
            _parentReferenceResolver = new MethodReferenceResolver(parentMethod);
        }

        [TestMethod]
        public void MethodInvocation_Without_Argument()
        {
            var mapper = GetMapper(GetElementFinderWithoutArgument());

            var mappedMethodInvocation = mapper.GetMappedItem();
            var arguments = mappedMethodInvocation.Arguments;
            var invokedMethodName = mappedMethodInvocation.Identifier.GetField();

            Assert.AreEqual(invokedMethodName, MethodName);
            Assert.IsFalse(arguments.Any());
        }

        [TestMethod]
        public void InstanceMethodInvocation_With_Argument()
        {
            var mapper = GetMapper(GetInstanceMethodInvocationElementFinder());

            var mappedMethodInvocation = mapper.GetMappedItem();
            var arguments = mappedMethodInvocation.Arguments;
            var invokedMethodName = mappedMethodInvocation.Identifier.GetField();

            Assert.IsTrue(mappedMethodInvocation.IsMemberOfParentClass);
            Assert.AreEqual(invokedMethodName, MethodName);
            Assert.IsTrue(arguments.Any());
        }

        [TestMethod]
        public void StaticMethodInvocation_With_Argument()
        {
            var mapper = GetMapper(GetStaticMethodInvocationElementFinder());

            var mappedMethodInvocation = mapper.GetMappedItem();
            var arguments = mappedMethodInvocation.Arguments;
            var invokedMethodName = mappedMethodInvocation.Identifier.GetField();

            Assert.IsFalse(mappedMethodInvocation.IsMemberOfParentClass);
            Assert.AreEqual(invokedMethodName, MethodName);
            Assert.IsTrue(arguments.Any());
        }

        private MethodInvocationMapper GetMapper(IMethodInvocationElementFinder elementFinder)
        {
            return new MethodInvocationMapper(elementFinder, _parentReferenceResolver);
        }

        private FakeMethodInvocationElementFinder GetElementFinderWithoutArgument()
        {
            string[] invokedMethodIdentifier = {_instancePointer, MethodName};
            return new FakeMethodInvocationElementFinder(invokedMethodIdentifier);
        }

        private FakeMethodInvocationElementFinder GetInstanceMethodInvocationElementFinder()
        {
            string[] invokedMethodIdentifier = {_instancePointer, MethodName};
            string[] arguments = {_instancePointer, _declaredFieldId};

            return new FakeMethodInvocationElementFinder(invokedMethodIdentifier, arguments);
        }


        private FakeMethodInvocationElementFinder GetStaticMethodInvocationElementFinder()
        {
            var staticClassName = "staticClass";
            string[] invokedMethodIdentifier = {staticClassName, MethodName};
            string[] arguments = {_instancePointer, _declaredFieldId};

            return new FakeMethodInvocationElementFinder(invokedMethodIdentifier, arguments);
        }
    }
}