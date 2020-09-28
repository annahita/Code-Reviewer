using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.Antlr.ElementFinders;
using Python.AST.Map.Methods;
using Python.AST.Map.Test.Helpers;
using Python.AST.Map.Test.Helpers.Models;
using Python.Model;

namespace Python.AST.Map.Test.Methods
{
    [TestClass]
    public class MethodBodyMapperTests
    {
        private const string newVariableId = "newLocalVariable";
        private const string newInstanceFieldId = "newInstanceField";


        private string _instancePointer;
        private IEnumerable<string> _parametersName;
        private MethodDeclaration _parentMethod;

        [TestInitialize]
        public void TestInit()
        {
            _parentMethod = new BuildDefaultModel().GetMethodWithEmptyBody();
            _instancePointer = _parentMethod.InstancePointer.Name;
            _parametersName = _parentMethod.Parameters.Select(a => a.Name);
        }


        [TestMethod]
        public void TestEmptyMethodBodyMapper()
        {
            var mapper = GetMapper(new FakeIMethodBodyElementFinder());

            var actualBody = mapper.GetMappedItem();
            var actualParentMethod = actualBody.Parent.Name;
            var statementsAreEmpty = !(actualBody.EmbeddedStatements.Any() || actualBody.SimpleStatements.Any());

            Assert.AreEqual(_parentMethod.Name, actualParentMethod);
            Assert.IsTrue(statementsAreEmpty);
        }

        [TestMethod]
        public void FirstTime_InstanceField_InLeftPartOfAssignment_ShouldBeTypeOf_Declaration()
        {
            var assignment = GetFakeAssignFinder(BuildFieldIdentifier(newInstanceFieldId));
            var mapper = GetMapper(new FakeIMethodBodyElementFinder(new[] {assignment}));

            var mappedBody = mapper.GetMappedItem();
            var newDeclaredField = mappedBody.GetDeclaredFields().FirstOrDefault();
            var actualNewFieldId = newDeclaredField.Identifier.GetField();

            Assert.AreEqual(actualNewFieldId, newInstanceFieldId);
        }

        [TestMethod]
        public void SecondTime_InstanceField_InLeftPartOfAssignment_ShouldBeTypeOf_Modification()
        {
            var firstLineAssignment = GetFakeAssignFinder(BuildFieldIdentifier(newInstanceFieldId));
            var secondTimeLineAssignment = GetFakeAssignFinder(BuildFieldIdentifier(newInstanceFieldId));
            var mapper =
                GetMapper(new FakeIMethodBodyElementFinder(new[] {firstLineAssignment, secondTimeLineAssignment}));

            var mappedBody = mapper.GetMappedItem();
            var modifiedField = mappedBody.GetModifiedFields().FirstOrDefault();
            var actualModifiedFieldId = modifiedField.Identifier.GetField();


            Assert.AreEqual(actualModifiedFieldId, newInstanceFieldId);
        }

        [TestMethod]
        public void FirstTime_LocalVariable_InLeftPartOfAssignment_ShouldBeTypeOf_Declaration()
        {
            var assignment = GetFakeAssignFinder(new[] {newVariableId});
            var mapper = GetMapper(new FakeIMethodBodyElementFinder(new[] {assignment}));

            var mappedBody = mapper.GetMappedItem();
            var newDeclaredVariable = mappedBody.GetDeclaredLocalVariables().FirstOrDefault();
            var actualNewVariableId = newDeclaredVariable.Identifier.GetField();

            Assert.AreEqual(actualNewVariableId, newVariableId);
        }

        [TestMethod]
        public void SecondTime_LocalVariable_InLeftPartOfAssignment_ShouldBeTypeOf_Declaration()
        {
            var firstLineAssignment = GetFakeAssignFinder(new[] {newVariableId});
            var secondTimeLineAssignment = GetFakeAssignFinder(new[] {newVariableId});
            var mapper =
                GetMapper(new FakeIMethodBodyElementFinder(new[] {firstLineAssignment, secondTimeLineAssignment}));

            var mappedBody = mapper.GetMappedItem();
            var modifiedVariable = mappedBody.GetModifiedFields().FirstOrDefault();
            var actualModifiedVariableId = modifiedVariable.Identifier.GetField();

            Assert.AreEqual(actualModifiedVariableId, newVariableId);
        }

        [TestMethod]
        public void SecondTime_variable_InLeftPartOfAssignment_ShouldBeTypeOf_Modification()
        {
            var assignment = GetFakeAssignFinder(BuildFieldIdentifier(newInstanceFieldId));
            var mapper = GetMapper(new FakeIMethodBodyElementFinder(new[] {assignment}));

            var mappedBody = mapper.GetMappedItem();
            var newDeclaredField = mappedBody.GetDeclaredFields().FirstOrDefault();
            var actualNewFieldId = newDeclaredField.Identifier.GetField();

            Assert.AreEqual(actualNewFieldId, newInstanceFieldId);
        }

        private MethodBodyMapper GetMapper(IMethodBodyElementFinder elementFinder)
        {
            return new MethodBodyMapper(elementFinder, _parentMethod);
        }

        private FakeAssignStatementElementFinder GetFakeAssignFinder(string[] identifier)
        {
            var leftPart = identifier;
            string[] rightPart = {_parametersName.FirstOrDefault()};

            return new FakeAssignStatementElementFinder(leftPart, rightPart);
        }

        private string[] BuildFieldIdentifier(string fieldId)
        {
            return new[] {_instancePointer, fieldId};
        }
    }
}