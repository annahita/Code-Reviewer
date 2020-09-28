using Moq;
using OOP_Model;

namespace CleanCode.Test.TestHelpers.Mocks
{
    public static class MockIEmbeddedStatement
    {
        private const short DefaultLinesInBlock = 1;

        public static IEmbeddedStatement MultiLevelNestedIEmbeddedStatements(int numberOfNestedStructures)
        {
            var embedded = new Mock<IEmbeddedStatement>();
            embedded.Setup(x => x.CountNestedStructures()).Returns(numberOfNestedStructures);
            embedded.Setup(x => x.GetLinesOfCodeCount()).Returns(DefaultLinesInBlock);
            return embedded.Object;
        }

        public static IEmbeddedStatement GeneralIEmbeddedStatement(int linesInBlock)
        {
            var embedded = new Mock<IEmbeddedStatement>();
            embedded.Setup(x => x.GetLinesOfCodeCount()).Returns(linesInBlock);
            return embedded.Object;
        }
    }
}