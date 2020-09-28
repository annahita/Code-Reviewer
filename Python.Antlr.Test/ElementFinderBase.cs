using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.Antlr.Test.ElementFinders.Fakes;
using Python.Antlr.Test.PythonCodeTemplates;

namespace Python.Antlr.Test
{
    [TestClass]
    public class ElementFinderBaseTests
    {
        [TestMethod]
        public void CountOfDirectStatementsInBodyTest()
        {
            const int statements = 2;
            const int functionSignatureLength = 1;
            var methodDef = GenerateMethod(statements);
            var parser = TestParser.GetParser(methodDef);
            var finder = new FakeBaseElementFinder(parser.funcdef(), parser);

            var startLine = finder.GetLineNumber();
            var endLine = finder.GetEndLineNumber();

            Assert.AreEqual(1, startLine);
            Assert.AreEqual(statements + functionSignatureLength, endLine);
        }

        private string GenerateMethod(int statementsCount)
        {
            var methodBody = GenerateMethodBody(statementsCount);
            return PythonCodeGenerator.GenerateFunction("ParentMethod", new string[] { }, methodBody, 0);
        }

        private string GenerateMethodBody(int statementsCount)
        {
            var statements = new List<string>();
            for (var i = 0; i < statementsCount; i++)
                statements.Add(PythonCodeGenerator.GenerateDefaultAssignmentStatement());

            return PythonCodeGenerator.GenerateFunctionBody(statements);
        }
    }
}