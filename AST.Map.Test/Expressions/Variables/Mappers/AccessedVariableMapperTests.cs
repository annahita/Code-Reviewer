using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.AST.Map.Expressions.Variables.Mappers;
using Python.AST.Map.Test.Helpers;
using Python.AST.Map.Test.Helpers.Models;

namespace Python.AST.Map.Test.Expressions.Variables.Mappers
{
    [TestClass]
    public class AccessedVariableMapperTests
    {
        private const string LocalVariableId = "localVar";
        private const string SubField = "subField";

        [TestMethod]
        public void AccessedVariableMapTest()
        {
            var accessedExpression =
                ExpressionBuilder.BuildLocalVariable(ExpressionBuilder.BuildIdentifier(new[] {LocalVariableId}));
            var identifier = ExpressionBuilder.BuildIdentifier(new[] {LocalVariableId, SubField});
            var mapper = new AccessedVariableMapper(new FakeIdentifierElementFinder(),
                identifier, accessedExpression);

            var mappedVariable = mapper.GetMappedItem();
            var actualIdentifier = mappedVariable.Identifier;
            var actualAccessedExpression = mappedVariable.AccessedExpression;

            Assert.AreEqual(accessedExpression, actualAccessedExpression);
            Assert.AreEqual(identifier, actualIdentifier);
        }
    }
}