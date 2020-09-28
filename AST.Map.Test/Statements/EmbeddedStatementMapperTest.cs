using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.AST.Map.Statements;
using Python.AST.Map.Test.Helpers;

namespace Python.AST.Map.Test.Statements
{
    [TestClass]
    public class EmbeddedStatementMapperTest
    {
        [TestMethod]
        public void SingleLevelEmbeddedStatementMapperTest()
        {
            const int expectedNestedLevel = 1;
            const int childCount = 0;
            var mapper = GetMapper(childCount);

            var mappedStatement = mapper.GetMappedItem();
            var actualNestedLevel = mappedStatement.CountNestedStructures();

            Assert.AreEqual(expectedNestedLevel, actualNestedLevel);
        }

        [TestMethod]
        public void NestedEmbeddedStatementMapperTest()
        {
            const int expectedNestedLevel = 6;
            const int childCount = 5;
            var mapper = GetMapper(childCount);

            var mappedStatement = mapper.GetMappedItem();
            var actualNestedLevel = mappedStatement.CountNestedStructures();

            Assert.AreEqual(expectedNestedLevel, actualNestedLevel);
        }

        private EmbeddedStatementMapper GetMapper(int nestedLevels)
        {
            var elementFinder = new FakeEmbeddedStatementElementFinder(nestedLevels);
            return new EmbeddedStatementMapper(elementFinder);
        }
    }
}