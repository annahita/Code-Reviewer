using Moq;
using OOP_Model;

namespace CleanCode.Test.TestHelpers.Mocks
{
    public static class MockVariableAccess
    {
        public static IVariableAccess ClassField(string[] identifiers)
        {
            var variable = GeneralVariable(identifiers);
            variable.Setup(x => x.IsMemberOfParentClass()).Returns(true);
            return variable.Object;
        }

        private static Mock<IVariableAccess> GeneralVariable(string[] identifier)
        {
            var identifieMock = MockIdentifier.FieldIdentifier(identifier);

            var variable = new Mock<IVariableAccess>();
            variable.Setup(x => x.Identifier).Returns(identifieMock);
            return variable;
        }
    }
}