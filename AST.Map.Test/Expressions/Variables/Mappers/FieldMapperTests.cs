using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.AST.Map.Expressions.Variables.Mappers;
using Python.AST.Map.Test.Helpers;
using Python.AST.Map.Test.Helpers.Models;

namespace Python.AST.Map.Test.Expressions.Variables.Mappers
{
    [TestClass]
    public class FieldMapperTests
    {
        private const string InstancePointer = "self";
        private const string Field = "Field";

        [TestMethod]
        public void ClassInstanceFieldMapTest()
        {
            var identifier = ExpressionBuilder.BuildIdentifier(new[] {Field}, InstancePointer);
            var mapper = new ClassInstanceFieldMapper(new FakeIdentifierElementFinder(),
                identifier);

            var mappedField = mapper.GetMappedItem();
            var actualIdentifier = mappedField.Identifier;
            var isInstanceVariable = mappedField.IsInstanceField;
            var isStatic = mappedField.IsStatic;

            Assert.AreEqual(identifier, actualIdentifier);
            Assert.IsTrue(isInstanceVariable);
            Assert.IsFalse(isStatic);
        }

        [TestMethod]
        public void ClassStaticFieldMapTest()
        {
            var identifier = ExpressionBuilder.BuildIdentifier(new[] {Field});
            var mapper = new ClassStaticFieldMapper(new FakeIdentifierElementFinder(),
                identifier);

            var mappedField = mapper.GetMappedItem();
            var actualIdentifier = mappedField.Identifier;
            var isStatic = mappedField.IsStatic;
            var isInstanceVariable = mappedField.IsInstanceField;


            Assert.AreEqual(identifier, actualIdentifier);
            Assert.IsTrue(isStatic);
            Assert.IsFalse(isInstanceVariable);
        }
    }
}