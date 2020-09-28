using System.Collections.Generic;
using System.Linq;
using Moq;
using OOP_Model;

namespace CleanCode.Test.TestHelpers.Mocks
{
    public static class MockBlock
    {
        public static IMethodBody GetMethodBodyThatAccessedClassFields(string[] accessedClassFields)
        {
            var accessedFields =
                accessedClassFields.Select(a => MockVariableAccess.ClassField(new[] {a}));
            var methodBody = new Mock<IMethodBody>();
            methodBody.Setup(x => x.GetAccessedFields()).Returns(accessedFields);
            return methodBody.Object;
        }

        public static IMethodBody GetMethodBodyThatModifiedClassFields(string[] modifiedClassFields)
        {
            var modifiedFields =
                modifiedClassFields.Select(a => MockVariableModification.ModifiedClassField(new[] {a}));
            var methodBody = new Mock<IMethodBody>();
            methodBody.Setup(x => x.GetModifiedFields()).Returns(modifiedFields);
            return methodBody.Object;
        }

        public static IMethodBody GetMethodBodyWithEmbeddedStatement(IEnumerable<IEmbeddedStatement> embeddedStatements)

        {
            var methodBody = new Mock<IMethodBody>();
            methodBody.Setup(x => x.EmbeddedStatements).Returns(embeddedStatements);
            return methodBody.Object;
        }

        public static IMethodBody GetMethodBodyThatInvokedInternalMethods(IEnumerable<string> invokedMethodsIdentifiers)

        {
            var invokedMethods = invokedMethodsIdentifiers.Select(a =>
                MockMethodInvocation.InternalInvokedMethod(new[] {a}));
            var methodBody = new Mock<IMethodBody>();
            methodBody.Setup(x => x.GetInvokedMethods()).Returns(invokedMethods);
            return methodBody.Object;
        }

        public static IMethodBody GetMethodBodyThatModifiedMethodParameters(string parameter)
        {
            var modifiedParameter = MockVariableModification.MethodParameter(new[] {parameter});
            var methodBody = new Mock<IMethodBody>();
            methodBody.Setup(x => x.GetModifiedFields()).Returns(new[] {modifiedParameter});
            return methodBody.Object;
        }

        public static IMethodBody GetMethodBodyThatJustModifiedLocalVariable(string localVariable)
        {
            var modifiedVariable = MockVariableModification.LocalVariable(new[] {localVariable});
            var methodBody = new Mock<IMethodBody>();
            methodBody.Setup(x => x.GetModifiedFields()).Returns(new[] {modifiedVariable});
            return methodBody.Object;
        }

        public static IMethodBody GetMethodBodyThatJustModifiedClassInstanceFields(string classField)
        {
            var modifiedClassField = MockVariableModification.ClassField(new[] {classField});
            var methodBody = new Mock<IMethodBody>();
            methodBody.Setup(x => x.GetModifiedFields()).Returns(new[] {modifiedClassField});
            return methodBody.Object;
        }

        public static IMethodBody GetMethodBodyThatModifiedStaticFields(string staticField)
        {
            var modifiedField = MockVariableModification.ClassStaticField(new[] {staticField});
            var methodBody = new Mock<IMethodBody>();
            methodBody.Setup(x => x.GetModifiedFields()).Returns(new[] {modifiedField});
            return methodBody.Object;
        }

        public static Mock<IMethodBody> GeneralMethodBody()
        {
            var methodBody = new Mock<IMethodBody>();
            return methodBody;
        }

        public static Mock<IMethodBody> GeneralMethodBodyWithBlockSize(int linesInBlock)
        {
            var methodBody = new Mock<IMethodBody>();
            methodBody.Setup(x => x.GetLinesOfCodeCount()).Returns(linesInBlock);
            return methodBody;
        }
    }
}