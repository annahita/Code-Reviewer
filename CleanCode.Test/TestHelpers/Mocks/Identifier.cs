using System.Linq;
using Moq;
using OOP_Model;

namespace CleanCode.Test.TestHelpers.Mocks
{
    public static class MockIdentifier
    {
        public static IIdentifier SingleIdentifier(string identifier)
        {
            var variable = new Mock<IIdentifier>();
            variable.Setup(x => x.Id).Returns(new[] {identifier});
            variable.Setup(x => x.GetTarget()).Returns(identifier);
            variable.Setup(x => x.GetField()).Returns(identifier);
            variable.Setup(x => x.GetIdentifiersAsString()).Returns(identifier);

            return variable.Object;
        }

        public static IIdentifier FieldIdentifier(string[] identifier)
        {
            var variable = new Mock<IIdentifier>();
            variable.Setup(x => x.Id).Returns(identifier);
            variable.Setup(x => x.GetTarget()).Returns(identifier.FirstOrDefault());
            variable.Setup(x => x.GetField()).Returns(identifier.LastOrDefault());
            variable.Setup(x => x.GetIdentifiersAsString()).Returns(string.Join(".", identifier));

            return variable.Object;
        }
    }
}