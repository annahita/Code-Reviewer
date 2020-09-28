using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.Antlr.ElementFinders;
using Python.Antlr.Test.PythonCodeTemplates;

namespace Python.Antlr.Test.ElementFinders
{
    [TestClass]
    public class EmbeddedStatementElementFinderTests
    {
        [TestMethod]
        public void FindNestedStatement()
        {
            var secondLevel = PythonCodeGenerator.GenerateDefaultCompoundStatement(2);
            var firstLevel = PythonCodeGenerator.GenerateDefaultNestedCompoundStatement(new[] {secondLevel});
            var finder = BuildFinder(firstLevel);

            var nestedLevels = finder.FindNestedEmbeddedStatements();

            Assert.AreEqual(1, nestedLevels.Count());
        }

        [TestMethod]
        public void FindCountOfDirectStatements()
        {
            var simpleStatement = PythonCodeGenerator.GenerateDefaultVariableDeclaration();
            var secondLevel = PythonCodeGenerator.GenerateDefaultCompoundStatement(2);
            var firstLevel =
                PythonCodeGenerator.GenerateDefaultNestedCompoundStatement(new[] {simpleStatement},
                    new[] {secondLevel});
            var finder = BuildFinder(firstLevel);

            var countOfDirectStatements = finder.CountOfDirectStatementsInBody();

            Assert.AreEqual(2, countOfDirectStatements);
        }

        private EmbeddedStatementElementFinder BuildFinder(string code)
        {
            var parser = TestParser.GetParser(code);

            return new EmbeddedStatementElementFinder(parser.compound_stmt(), parser);
        }
    }
}