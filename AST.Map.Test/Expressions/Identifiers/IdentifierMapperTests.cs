using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.AST.Map.Expressions.Identifiers;
using Python.AST.Map.RelationResolver;
using Python.AST.Map.Test.Helpers;
using Python.AST.Map.Test.Helpers.Models;

namespace Python.AST.Map.Test.Expressions.Identifiers
{
    [TestClass]
    public class IdentifierMapperTests
    {
        private string _instancePointer;
        private IParentReferenceResolver _parentReferenceResolver;

        [TestInitialize]
        public void TestInit()
        {
            var defaultMethod = new BuildDefaultModel().GetMethod();
            _instancePointer = defaultMethod.InstancePointer.Name;
            _parentReferenceResolver = new MethodReferenceResolver(defaultMethod);
        }

        [TestMethod]
        public void MapInstanceFieldId()
        {
            const string field = "testField";
            var mapper = GetNewIdentifierMapper(new[] {_instancePointer, field});
            var identifier = mapper.GetMappedItem();

            Assert.AreEqual(_instancePointer, identifier.InstancePointer);
            Assert.AreEqual(identifier.GetTarget(), field);
        }

        [TestMethod]
        public void Field_HasNotInstancePointer_MappedIdentifierInstancePointer_ShouldBeEmpty()
        {
            string[] fields = {"target", "testField"};
            var mapper = GetNewIdentifierMapper(fields);

            var identifier = mapper.GetMappedItem();

            Assert.AreEqual(string.Empty, identifier.InstancePointer);
            Assert.AreEqual(identifier.GetTarget(), fields[0]);
            Assert.AreEqual(identifier.GetField(), fields[1]);
        }

        [TestMethod]
        public void Id_ShouldNotInclude_InstancePointer()
        {
            const string instancePointer = "self";
            const string field = "testField";
            var mapper = GetNewIdentifierMapper(new[] {instancePointer, field});

            var identifier = mapper.GetMappedItem();
            var mainId = identifier.Id.ToList();

            CollectionAssert.DoesNotContain(mainId, identifier.InstancePointer);
        }

        private IdentifierMapper GetNewIdentifierMapper(string[] identifier)
        {
            var elementFinder = new FakeIdentifierElementFinder(identifier);
            return new IdentifierMapper(elementFinder, _parentReferenceResolver);
        }
    }
}