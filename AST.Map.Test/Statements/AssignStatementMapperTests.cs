using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.Antlr.ElementFinders;
using Python.AST.Map.RelationResolver;
using Python.AST.Map.Statements;
using Python.AST.Map.Test.Helpers;
using Python.AST.Map.Test.Helpers.Models;
using Python.Model;
using Python.Model.Extension;

namespace Python.AST.Map.Test.Statements
{
    [TestClass]
    public class AssignStatementMapperTests
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
        public void VariableDeclarationStatementMapTest()
        {
            var mapper = GetMapper(GetVariableDeclarationStatement());

            var statement = mapper.GetMappedItem();
            var declaredVariableInLeftPart = statement.LeftExpressions.FirstOrDefault();

            Assert.IsInstanceOfType(declaredVariableInLeftPart, typeof(LocalVariableDeclaration));
        }

        [TestMethod]
        public void VariableModificationStatementMapTest()
        {
            var mapper = GetMapper(GetVariableModificationStatement());

            var statement = mapper.GetMappedItem();
            var declaredVariableInLeftPart = statement.LeftExpressions.FirstOrDefault();

            Assert.IsInstanceOfType(declaredVariableInLeftPart, typeof(VariableModification));
        }

        [TestMethod]
        public void RightPartExpressionsMapTest()
        {
            var mapper = GetMapper(GetVariableDeclarationStatement());

            var statement = mapper.GetMappedItem();
            var rightPart = statement.RightExpressions;
            var variablesInRightPart = rightPart.FilterExpression<VariableAccess>();
            var methodInvocationInRightPart = rightPart.FilterExpression<MethodInvocation>();

            Assert.IsTrue(variablesInRightPart.Any());
            Assert.IsTrue(methodInvocationInRightPart.Any());
        }

        private AssignStatementMapper GetMapper(IAssignStatementElementFinder elementFinder)
        {
            return new AssignStatementMapper(elementFinder, _parentReferenceResolver);
        }

        private FakeAssignStatementElementFinder GetVariableDeclarationStatement()
        {
            string[] leftPart = {NewVariable};
            string[] variableInRightPart = {_instancePointer, _declaredFieldId};
            string[] methodInvocationInRightPart = {_instancePointer, InvokedMethodName};

            return new FakeAssignStatementElementFinder(leftPart, variableInRightPart, methodInvocationInRightPart);
        }

        private FakeAssignStatementElementFinder GetVariableModificationStatement()
        {
            string[] leftPart = {_instancePointer, _declaredFieldId};
            string[] variableInRightPart = {NewVariable};
            string[] methodInvocationInRightPart = {_instancePointer, InvokedMethodName};

            return new FakeAssignStatementElementFinder(leftPart, variableInRightPart, methodInvocationInRightPart);
        }
    }
}