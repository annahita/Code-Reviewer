using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.AST.Map.RelationResolver;
using Python.AST.Map.Statements;
using Python.AST.Map.Test.Helpers;
using Python.AST.Map.Test.Helpers.Models;
using Python.Model;
using Python.Model.Extension;

namespace Python.AST.Map.Test.Statements
{
    [TestClass]
    public class SingleStatementMapperTests
    {
        private const string InvokedMethodName = "invokedMethod";
        private const string NewVariable = "newLocalVariable";
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
        public void StatementMapperTest()
        {
            var mapper = GetMapper();

            var mapedStatement = mapper.GetMappedItem();
            var mappedExpressions = mapedStatement.Expressions;
            var variableExpressions = mappedExpressions.FilterExpression<VariableAccess>();
            var methodInvocations = mappedExpressions.FilterExpression<MethodInvocation>();

            Assert.IsTrue(variableExpressions.Any());
            Assert.IsTrue(methodInvocations.Any());
        }


        private SinglePartStatementMapper GetMapper()
        {
            string[] variableId = {NewVariable};
            string[] methodInvocationId = {_instancePointer, InvokedMethodName};

            var elementFinder = new FakeStatementElementFinder(methodInvocationId, variableId);
            return new SinglePartStatementMapper(elementFinder, _parentReferenceResolver);
        }
    }
}