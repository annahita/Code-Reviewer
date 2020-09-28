using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.Antlr.ElementFinders;

namespace Python.Antlr.Test.ElementFinders
{
    [TestClass]
    public class IdentifierElementFinderTest
    {
        [TestMethod]
        public void FindSingleIdentifier()
        {
            var singleId = "singleId";
            var finder = BuildFinder(singleId);

            var identifiers = finder.FindIdentifier().ToList();

            Assert.AreEqual(1, identifiers.Count());
            Assert.AreEqual(singleId, identifiers.FirstOrDefault());
        }

        [TestMethod]
        public void FindMultiPartIdentifier()
        {
            const string target = "target";
            const string field = "field";
            const string multiPartId = target + "." + field;
            var finder = BuildFinder(multiPartId);

            var identifiers = finder.FindIdentifier().ToList();

            Assert.AreEqual(target, identifiers[0]);
            Assert.AreEqual(field, identifiers[1]);
        }


        private IdentifierElementFinder BuildFinder(string code)
        {
            var parser = TestParser.GetParser(code);

            return new IdentifierElementFinder(parser.expr_element(), parser);
        }
    }
}