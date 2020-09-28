using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.AST.Map.Expressions.Variables.Mappers;
using Python.AST.Map.Test.Helpers;
using Python.AST.Map.Test.Helpers.Models;

namespace Python.AST.Map.Test.Expressions.Variables.Mappers
{
    [TestClass]
    public class ModifiedVariableMapperTests
    {
        private const string LocalVariableId = "localVar";
        private const string SubField = "subField";

        [TestMethod]
        public void ModifiedVariableMapTest()
        {
            var modifiedExpression =
                ExpressionBuilder.BuildLocalVariable(ExpressionBuilder.BuildIdentifier(new[] {LocalVariableId}));
            var identifier = ExpressionBuilder.BuildIdentifier(new[] {LocalVariableId, SubField});
            var mapper = new ModifiedVariableMapper(new FakeIdentifierElementFinder(),
                identifier, modifiedExpression);

            var mappedVariable = mapper.GetMappedItem();
            var actualIdentifier = mappedVariable.Identifier;
            var actualModifiedExpression = mappedVariable.ModifiedExpression;

            Assert.AreEqual(modifiedExpression, actualModifiedExpression);
            Assert.AreEqual(identifier, actualIdentifier);
        }
    }
}