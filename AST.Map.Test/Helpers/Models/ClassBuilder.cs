using Python.Model;

namespace Python.AST.Map.Test.Helpers.Models
{
    public static class ClassBuilder
    {
        private const int DefaultLineNumber = 1;

        public static ClassDeclaration BuildMethod(string name)
        {
            var classDeclaration = new ClassDeclaration(DefaultLineNumber);
            classDeclaration.SetName(name);

            return classDeclaration;
        }
    }
}