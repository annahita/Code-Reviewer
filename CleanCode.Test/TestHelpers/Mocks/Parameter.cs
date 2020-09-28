using System.Collections.Generic;
using Moq;
using OOP_Model;

namespace CleanCode.Test.TestHelpers.Mocks
{
    public static class MockParameter
    {
        private const short DefaultVariableLength = 5;


        private static IParameterDeclaration ParameterDeclaration(string name)
        {
            var variable = new Mock<IParameterDeclaration>();
            variable.Setup(x => x.Name).Returns(name);
            return variable.Object;
        }

        public static IEnumerable<IParameterDeclaration> ParameterDeclarations(int numberOfVariables)
        {
            var variablesLit = new List<IParameterDeclaration>();
            for (var i = 0; i < numberOfVariables; i++)
            {
                var variableIdentifier = NameHelper.RandomString(DefaultVariableLength);
                variablesLit.Add(ParameterDeclaration(variableIdentifier));
            }

            return variablesLit;
        }
    }
}