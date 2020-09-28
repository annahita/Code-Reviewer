using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.Antlr.ElementFinders;
using Python.Antlr.Test.PythonCodeTemplates;

namespace Python.Antlr.Test.ElementFinders
{
    [TestClass]
    public class SinglePartStatementElementFinderTests
    {
        [TestMethod]
        public void FindMethodInvocationsInSingleStatement()
        {
            var methodInvocation = PythonCodeGenerator.GenerateDefaultMethodInvocation(0);
            var finder = BuildFinder(methodInvocation);

            var invokedMethods = finder.FindMethodInvocations();

            Assert.AreEqual(1, invokedMethods.Count());
        }

        [TestMethod]
        public void FindVariablesInReturnStatement()
        {
            var variableId = "variable1";
            var returnStatement = PythonCodeGenerator.GenerateReturnStatement(variableId, 0);
            var finder = BuildFinder(returnStatement);

            var variables = finder.FindVariables();

            Assert.AreEqual(1, variables.Count());
        }

        private SinglePartStatementElementFinder BuildFinder(string code)
        {
            var parser = TestParser.GetParser(code);

            return new SinglePartStatementElementFinder(parser.small_stmt(), parser);
        }
    }
}