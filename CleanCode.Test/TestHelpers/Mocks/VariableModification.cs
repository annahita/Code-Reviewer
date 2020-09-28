using Moq;
using OOP_Model;

namespace CleanCode.Test.TestHelpers.Mocks
{
    public static class MockVariableModification
    {
        public static IVariableModification ClassField(string[] identifiers)
        {
            var variable = GeneralVariable(identifiers);
            variable.Setup(x => x.IsMemberOfParentClass()).Returns(true);
            return variable.Object;
        }

        public static IVariableModification ModifiedClassField(string[] identifiers)
        {
            var variable = GeneralVariable(identifiers);
            variable.Setup(x => x.IsMemberOfParentClass()).Returns(true);
            return variable.Object;
        }


        public static IVariableModification ClassStaticField(string[] identifiers)
        {
            var variable = GeneralVariable(identifiers);
            variable.Setup(x => x.IsStatic()).Returns(true);
            return variable.Object;
        }

        public static IVariableModification MethodParameter(string[] identifiers)
        {
            var variable = GeneralVariable(identifiers);
            variable.Setup(x => x.IsMethodParameter()).Returns(true);
            return variable.Object;
        }

        public static IVariableModification LocalVariable(string[] identifiers)
        {
            var variable = GeneralVariable(identifiers);
            variable.Setup(x => x.IsLocalVariable()).Returns(true);
            return variable.Object;
        }

        private static Mock<IVariableModification> GeneralVariable(string[] identifier)
        {
            var identifieMock = MockIdentifier.FieldIdentifier(identifier);

            var variable = new Mock<IVariableModification>();
            variable.Setup(x => x.Identifier).Returns(identifieMock);
            return variable;
        }
    }
}