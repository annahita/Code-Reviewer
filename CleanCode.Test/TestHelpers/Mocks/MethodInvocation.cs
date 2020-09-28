using Moq;
using OOP_Model;

namespace CleanCode.Test.TestHelpers.Mocks
{
    public static class MockMethodInvocation
    {
        public static IMethodInvocation InternalInvokedMethod(string[] identifiers)
        {
            var invokedMethod = GeneralInvokedMethod(identifiers);
            invokedMethod.Setup(x => x.IsMemberOfParentClass).Returns(true);
            return invokedMethod.Object;
        }


        private static Mock<IMethodInvocation> GeneralInvokedMethod(string[] identifiers)
        {
            var identifiedMock = MockIdentifier.FieldIdentifier(identifiers);
            var invokedMethod = new Mock<IMethodInvocation>();
            invokedMethod.Setup(x => x.Identifier).Returns(identifiedMock);

            return invokedMethod;
        }
    }
}