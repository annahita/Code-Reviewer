using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.Antlr.Test.PythonCodeTemplates;

namespace Python.Antlr.Test.ElementFinders
{
    [TestClass]
    public class RootElementFinderTests
    {
        [TestMethod]
        public void ShouldDetectAllClassesInCode()
        {
            string[] classNames = {"class1", "class2"};
            var code = PythonCodeGenerator.GenerateRoot(classNames);

            var parserBuilder = new ParserBuilder();
            parserBuilder.Build(code);

            var finder = parserBuilder.GetParseElementFinder();
            var classes = finder.FindClasses();
            var actualDetectedClasses = classes.Count();

            Assert.AreEqual(classNames.Length, actualDetectedClasses);
        }

        [TestMethod]
        public void EmptyCodeReturnEmptyRootElement()
        {
            var code = string.Empty;

            var parserBuilder = new ParserBuilder();
            parserBuilder.Build(code);

            var finder = parserBuilder.GetParseElementFinder();
            var classes = finder.FindClasses();
            var isEmpty = !classes.Any();

            Assert.IsTrue(isEmpty);
        }
    }
}