using System.Linq;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime.Tree.Xpath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.Antlr.ElementFinders;
using Python.Antlr.Test.PythonCodeTemplates;

namespace Python.Antlr.Test.ElementFinders
{
    [TestClass]
    public class MethodElementFinderTests
    {
        private const string MethodName = "NewMethod";
        private readonly string[] _parameters = {"param1", "param2"};

        [TestMethod]
        public void FindNameTest()
        {
            var elementFinder = BuildFinder(GenerateMethod());
            var methodName = elementFinder.FindName();
            Assert.AreEqual(MethodName, methodName);
        }

        [TestMethod]
        public void FindParametersTest()
        {
            var elementFinder = BuildFinder(GenerateMethod());

            var parameters = elementFinder.FindParameters();
            var foundParameter1 = parameters.FirstOrDefault().FindParameterName();
            var foundParameter2 = parameters.LastOrDefault().FindParameterName();

            Assert.AreEqual(_parameters.FirstOrDefault(), foundParameter1);
            Assert.AreEqual(_parameters.LastOrDefault(), foundParameter2);
        }

        [TestMethod]
        public void FindBodyTest()
        {
            var elementFinder = BuildFinder(GenerateMethod());

            var actualBody = elementFinder.FindBody();

            Assert.IsNotNull(actualBody);
        }

        private MethodElementFinder BuildFinder(string methodDef)
        {
            var classDef = PythonCodeGenerator.GenerateClass("ParentClass", new string[] { }, methodDef);
            var parser = TestParser.GetParser(classDef);

            return new MethodElementFinder(GetFunctionTree(parser), parser);
        }

        private string GenerateMethod()
        {
            var statement = PythonCodeGenerator.GenerateDefaultVariableDeclaration();
            var methodBody = PythonCodeGenerator.GenerateFunctionBody(new[] {statement});
            return PythonCodeGenerator.GenerateFunction(MethodName, _parameters, methodBody);
        }

        private IParseTree GetFunctionTree(PythonParser parser)
        {
            var parserTree = parser.root();
            var elementTree = XPath.FindAll(parserTree, "//funcdef", parser);
            return elementTree.FirstOrDefault();
        }
    }
}