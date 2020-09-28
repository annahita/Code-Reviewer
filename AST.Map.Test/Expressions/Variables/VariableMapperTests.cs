using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.AST.Map.Expressions.Variables;
using Python.AST.Map.Expressions.Variables.Strategies;
using Python.AST.Map.RelationResolver;
using Python.AST.Map.Test.Helpers;
using Python.AST.Map.Test.Helpers.Models;
using Python.Model;

namespace Python.AST.Map.Test.Expressions.Variables
{
    [TestClass]
    public class VariableMapperTests
    {
        private IParentReferenceResolver _classReferenceResolver;
        private string _declaredFieldId;
        private string _instancePointer;
        private IParentReferenceResolver _methodReferenceResolver;

        [TestInitialize]
        public void TestInit()
        {
            var parentMethod = new BuildDefaultModel().GetMethod();
            var declaredField = parentMethod.ParentClass.Fields.FirstOrDefault();
            _declaredFieldId = declaredField.Identifier.GetField();
            _instancePointer = parentMethod.InstancePointer.Name;
            _methodReferenceResolver = new MethodReferenceResolver(parentMethod);
            _classReferenceResolver = new ClassReferenceResolver(parentMethod.ParentClass);
        }


        [TestMethod]
        public void AccessedMember_WithAlteredStrategy_ShouldMapAs_ModifiedVariable()
        {
            string[] accessedFieldId = {_instancePointer, _declaredFieldId};
            var alteredStrategy = new AlteredVariableStrategy();
            var mapper = new VariableMapperBridge(new FakeIdentifierElementFinder(accessedFieldId),
                _methodReferenceResolver, alteredStrategy);
            var mappedVariable = mapper.GetMappedItem();

            Assert.IsInstanceOfType(mappedVariable, typeof(VariableModification));
        }

        [TestMethod]
        public void UndefinedInstanceField_WithAlteredStrategy_ShouldMapAs_FieldDeclaration()
        {
            string[] undefinedField = {_instancePointer, "undefinedClassInstanceField"};
            var alteredStrategy = new AlteredVariableStrategy();
            var mapper = new VariableMapperBridge(new FakeIdentifierElementFinder(undefinedField),
                _methodReferenceResolver, alteredStrategy);

            var mappedVariable = mapper.GetMappedItem();

            Assert.IsInstanceOfType(mappedVariable, typeof(FieldDeclaration));
        }

        [TestMethod]
        public void UndefinedStaticField_WithAlteredStrategy_ShouldMapAs_FieldDeclaration()
        {
            string[] undefinedField = {"undefinedClassStaticField"};
            var alteredStrategy = new AlteredVariableStrategy();
            var mapper = new VariableMapperBridge(new FakeIdentifierElementFinder(undefinedField),
                _classReferenceResolver, alteredStrategy);

            var mappedVariable = mapper.GetMappedItem();

            Assert.IsInstanceOfType(mappedVariable, typeof(FieldDeclaration));
        }

        [TestMethod]
        public void UndefinedLocalVariable_WithAlteredStrategy_ShouldMapAs_LocalVariableDeclaration()
        {
            string[] undefinedVariable = {"undefinedLocalVariable"};
            var alteredStrategy = new AlteredVariableStrategy();
            var mapper = new VariableMapperBridge(new FakeIdentifierElementFinder(undefinedVariable),
                _methodReferenceResolver, alteredStrategy);

            var mappedVariable = mapper.GetMappedItem();

            Assert.IsInstanceOfType(mappedVariable, typeof(LocalVariableDeclaration));
        }

        [TestMethod]
        public void UndefinedField_WithAlteredStrategy_ShouldMapAs_ModifiedVariable()
        {
            string[] undefined = {"undefinedTarget", "undefinedField"};
            var alteredStrategy = new AlteredVariableStrategy();
            var mapper = new VariableMapperBridge(new FakeIdentifierElementFinder(undefined),
                _methodReferenceResolver, alteredStrategy);

            var mappedVariable = mapper.GetMappedItem();

            Assert.IsInstanceOfType(mappedVariable, typeof(VariableModification));
        }

        [TestMethod]
        public void AccessedMember_WithAccessedStrategy_ShouldMapAs_ModifiedVariable()
        {
            var accessedStrategy = new AccessedVariableStrategy();
            var mapper = new VariableMapperBridge(new FakeIdentifierElementFinder(new[] {_declaredFieldId}),
                _methodReferenceResolver, accessedStrategy);

            var mappedVariable = mapper.GetMappedItem();

            Assert.IsInstanceOfType(mappedVariable, typeof(VariableAccess));
        }
    }
}