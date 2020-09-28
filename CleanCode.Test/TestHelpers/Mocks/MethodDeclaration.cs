using System.Collections.Generic;
using Moq;
using OOP_Model;

namespace CleanCode.Test.TestHelpers.Mocks
{
    public static class MockMethod
    {
        private const string DefaultMethodName = "defualtMethodName";

        public static IMethodBase EmptyMethod(string identifier)
        {
            var body = MockBlock.GeneralMethodBody();
            var method = new Mock<IMethodBase>();
            method.Setup(x => x.Name).Returns(identifier);
            method.Setup(x => x.MethodBody).Returns(body.Object);
            return method.Object;
        }

        public static IMethodBase Method(string identifier, IMethodBody body)
        {
            var method = GeneralIMethodBase(identifier, body);
            return method.Object;
        }

        public static IMethodBase Method(string identifier, IEnumerable<IParameterDeclaration> parameters)

        {
            var method = new Mock<IMethodBase>();
            method.Setup(x => x.Name).Returns(identifier);
            method.Setup(x => x.Parameters).Returns(parameters);
            return method.Object;
        }

        public static IMethodBase Method(string identifier, IEnumerable<IParameterDeclaration> parameters,
            IMethodBody body)
        {
            var method = GeneralIMethodBase(identifier, body);
            method.Setup(x => x.Parameters).Returns(parameters);
            return method.Object;
        }


        public static IMethodBase DefaultMethodWithBody(IMethodBody body)
        {
            var method = GeneralIMethodBase(DefaultMethodName, body);
            return method.Object;
        }

        private static Mock<IMethodBase> GeneralIMethodBase(string identifier, IMethodBody body)

        {
            var method = new Mock<IMethodBase>();
            method.Setup(x => x.Name).Returns(identifier);
            method.Setup(x => x.MethodBody).Returns(body);
            return method;
        }
    }
}