using System.Linq;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime.Tree.Xpath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.Antlr.ElementFinders;
using Python.Antlr.Test.PythonCodeTemplates;

namespace Python.Antlr.Test.ElementFinders
{
    [TestClass]
    public class MethodBodyElementFinderTests
    {
        [TestMethod]
        public void FindEmbeddedStatementsTest()
        {
            var methodBody = GenerateMethodBody(PythonCodeGenerator.GenerateDefaultCompoundStatement(2));
            var finder = BuildFinder(methodBody);

            var embeddedStatements = finder.FindEmbeddedStatements();
            Assert.AreEqual(1, embeddedStatements.Count());
        }

        [TestMethod]
        public void FindAssignStatementsTest()
        {
            var methodBody = GenerateMethodBody(PythonCodeGenerator.GenerateDefaultAssignmentStatement());
            var finder = BuildFinder(methodBody);

            var assignStatements = finder.FindAssignStatements();
            Assert.AreEqual(1, assignStatements.Count());
        }

        [TestMethod]
        public void FindSingleStatementsTest()
        {
            var methodBody = GenerateMethodBody(PythonCodeGenerator.GenerateDefaultSingleStatement());
            var finder = BuildFinder(methodBody);

            var singleStatements = finder.FindSingleStatements();
            Assert.AreEqual(1, singleStatements.Count());
        }

        [TestMethod]
        public void FindReturnStatementsTest()
        {
            var methodBody = GenerateMethodBody(PythonCodeGenerator.GenerateDefaultReturnStatement());
            var finder = BuildFinder(methodBody);

            var returnStatements = finder.FindReturnStatements();
            Assert.AreEqual(1, returnStatements.Count());
        }

        [TestMethod]
        public void CountOfDirectStatementsInBodyTest()
        {
            var assignmentStatement = PythonCodeGenerator.GenerateDefaultAssignmentStatement();
            var embeddedStatement = PythonCodeGenerator.GenerateDefaultCompoundStatement(2);
            var returnStatement = PythonCodeGenerator.GenerateDefaultReturnStatement();
            string[] statements = {assignmentStatement, embeddedStatement, returnStatement};
            var methodBody = GenerateMethodBody(statements);

            var finder = BuildFinder(methodBody);

            var statementsCount = finder.CountOfDirectStatementsInBody();
            Assert.AreEqual(3, statementsCount);
        }

        private MethodBodyElementFinder BuildFinder(string methodBody)
        {
            var methodDef = PythonCodeGenerator.GenerateFunction("ParentMethod", new string[] { }, methodBody, 0);
            var parser = TestParser.GetParser(methodDef);

            return new MethodBodyElementFinder(GetFunctionBodyTree(parser), parser);
        }

        private string GenerateMethodBody(string statement)
        {
            return PythonCodeGenerator.GenerateFunctionBody(new[] {statement});
        }

        private string GenerateMethodBody(string[] statements)
        {
            return PythonCodeGenerator.GenerateFunctionBody(statements);
        }

        private IParseTree GetFunctionBodyTree(PythonParser parser)
        {
            var parserTree = parser.funcdef();
            var elementTree = XPath.FindAll(parserTree, "/*/suite", parser);
            return elementTree.FirstOrDefault();
        }
    }
}