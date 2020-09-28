using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOP_Model;
using Python.AST.Map.Expressions;
using Python.AST.Map.RelationResolver;
using Python.AST.Map.Test.Helpers;
using Python.AST.Map.Test.Helpers.Models;
using Python.Model;
using Python.Model.Extension;

namespace Python.AST.Map.Test.Expressions
{
    [TestClass]
    public class ArgumentWithVariableTypeMapTests
    {
        private readonly string[] _methodNames = {"method1", "method2"};
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
        public void MapArgumentWithVariableTypeTest()
        {
            var mapper = GetArgumentMapper();

            var mappedExpressions = mapper.GetMappedItem().ToList();
            var mappedVariableArgument = mappedExpressions.FilterExpression<IVariableAccess>().FirstOrDefault();
            var actualArgumentId = mappedVariableArgument.Identifier.GetField();

            Assert.AreEqual(actualArgumentId, _declaredFieldId);
        }

        [TestMethod]
        public void MapArgumentWithMethodInvocationTypeTest()
        {
            var mapper = GetArgumentMapper();

            var mappedExpressions = mapper.GetMappedItem().ToList();
            var mappedMethodInvocationArgument =
                mappedExpressions.FilterExpression<MethodInvocation>().FirstOrDefault();
            var actualArgumentId = mappedMethodInvocationArgument.Identifier.GetField();

            Assert.AreEqual(actualArgumentId, _methodNames[1]);
        }

        private MethodArgumentMapper GetArgumentMapper()
        {
            var elementFinder = GetElementFinder();
            return new MethodArgumentMapper(elementFinder.FindArgumentCollection(), _parentReferenceResolver);
        }

        private FakeMethodInvocationElementFinder GetElementFinder()
        {
            string[] invokedMethodIdentifier = {_instancePointer, _methodNames[0]};
            string[] argumentWithVariableType = {_instancePointer, _declaredFieldId};
            string[] argumentWithMethodInvocationType = {_instancePointer, _methodNames[1]};

            return new FakeMethodInvocationElementFinder(invokedMethodIdentifier, argumentWithVariableType,
                argumentWithMethodInvocationType);
        }
    }
}