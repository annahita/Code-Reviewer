using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.Antlr.ElementFinders;
using Python.Antlr.Test.PythonCodeTemplates;

namespace Python.Antlr.Test.ElementFinders
{
    [TestClass]
    public class MethodInvocationElementFinderTests
    {
        [TestMethod]
        public void FindMethodInvocationWithVariableArguments()
        {
            const string methodName = "invokedMethod1";
            var arguments = new[] {"arg1", "arg2"};
            var methodInvocation = PythonCodeGenerator.GenerateMethodInvocation(methodName, arguments, 0);

            var finder = BuildFinder(methodInvocation);
            var invokedMethodName = finder.FindIdentifier().FindIdentifier().FirstOrDefault();
            var argumentsWithVariableType = finder.FindArgumentCollection().FindArgumentsWithVariableType();


            Assert.AreEqual(methodName, invokedMethodName);
            Assert.AreEqual(2, argumentsWithVariableType.Count());
        }


        [TestMethod]
        public void FindMethodInvocation_WithMethodInvocation_Arguments()
        {
            const string nestedInvokedMethodName = "argInvokedMethod";
            var nestedArguments = new[] {"arg1", "arg2"};
            var mainArgument =
                PythonCodeGenerator.GenerateMethodInvocation(nestedInvokedMethodName, nestedArguments, 0);

            const string mainInvokedMethodName = "invokedMethod1";
            var methodInvocation =
                PythonCodeGenerator.GenerateMethodInvocation(mainInvokedMethodName, new[] {mainArgument}, 0);

            var finder = BuildFinder(methodInvocation);
            var argumentsWithWithMethodInvocationType =
                finder.FindArgumentCollection().FindArgumentsWithMethodInvocationType();
            var argumentsWithWithVariableType = finder.FindArgumentCollection().FindArgumentsWithVariableType();

            Assert.AreEqual(1, argumentsWithWithMethodInvocationType.Count());
            Assert.AreEqual(0, argumentsWithWithVariableType.Count());
        }

        [TestMethod]
        public void FindMethodInvocation_With_EmptyArguments()
        {
            const string methodName = "invokedMethod1";
            var methodInvocation = PythonCodeGenerator.GenerateMethodInvocation(methodName, new string[] { }, 0);

            var finder = BuildFinder(methodInvocation);
            var argumentsWithWithMethodInvocationType =
                finder.FindArgumentCollection().FindArgumentsWithMethodInvocationType();
            var argumentsWithWithVariableType = finder.FindArgumentCollection().FindArgumentsWithVariableType();

            Assert.AreEqual(0, argumentsWithWithMethodInvocationType.Count());
            Assert.AreEqual(0, argumentsWithWithVariableType.Count());
        }

        private MethodInvocationElementFinder BuildFinder(string code)
        {
            var parser = TestParser.GetParser(code);

            return new MethodInvocationElementFinder(parser.expr_element(), parser);
        }
    }
}