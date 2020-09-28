using Moq;
using OOP_Model;

namespace CleanCode.Test.TestHelpers.Mocks
{
    public static class MockClassField
    {
        public static IFieldDeclaration ClassInstanceField(string identifier)
        {
            var identifierMock = MockIdentifier.SingleIdentifier(identifier);
            var variable = new Mock<IFieldDeclaration>();
            variable.Setup(x => x.Identifier).Returns(identifierMock);
            return variable.Object;
        }
    }
}