using System.Collections.Generic;
using System.Linq;
using Moq;
using OOP_Model;

namespace CleanCode.Test.TestHelpers.Mocks
{
    public static class MockClass
    {
        private const string DefaultClassName = "DefaultClass";

        public static IClassDeclaration EmptyClass(string name)
        {
            var mockedClass = GeneralClass(name);
            return mockedClass.Object;
        }

        public static IClassDeclaration GetClass(IEnumerable<string> classFields, IEnumerable<IMethodBase> methods)

        {
            var classFieldsDeclarations =
                classFields.Select(a => MockClassField.ClassInstanceField(a));
            var mockedClass = GeneralClass(DefaultClassName);
            mockedClass.Setup(x => x.Fields).Returns(classFieldsDeclarations);
            mockedClass.Setup(x => x.Methods).Returns(methods);
            return mockedClass.Object;
        }

        private static Mock<IClassDeclaration> GeneralClass(string name)
        {
            var mockedClass = new Mock<IClassDeclaration>();
            mockedClass.Setup(x => x.Name).Returns(name);
            return mockedClass;
        }
    }
}