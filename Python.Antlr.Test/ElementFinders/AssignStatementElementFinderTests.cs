using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.Antlr.ElementFinders;
using Python.Antlr.Test.PythonCodeTemplates;

namespace Python.Antlr.Test.ElementFinders
{
    [TestClass]
    public class AssignStatementElementFinderTests
    {
        [TestMethod]
        public void FindMethodInvocation()
        {
            const string variableId = "newVariable";
            var methodInvocation = PythonCodeGenerator.GenerateDefaultMethodInvocation(0);
            var statement = PythonCodeGenerator.GenerateAssignStatement(variableId, methodInvocation, 0);
            var finder = BuildFinder(statement);

            var invokedMethods = finder.FindMethodInvocations();

            Assert.AreEqual(1, invokedMethods.Count());
        }

        [TestMethod]
        public void FindVariables()
        {
            const string leftPartVariableId = "newVariable";
            var rightPartVariableId = "variable1";
            var statement = PythonCodeGenerator.GenerateAssignStatement(leftPartVariableId, rightPartVariableId, 0);
            var finder = BuildFinder(statement);

            var leftVariables = finder.FindVariablesInLeftPart();
            var rightVariables = finder.FindVariablesInRightPart();

            Assert.AreEqual(1, leftVariables.Count());
            Assert.AreEqual(1, rightVariables.Count());
        }

        private AssignStatementElementFinder BuildFinder(string code)
        {
            var parser = TestParser.GetParser(code);

            return new AssignStatementElementFinder(parser.small_stmt(), parser);
        }
    }
}