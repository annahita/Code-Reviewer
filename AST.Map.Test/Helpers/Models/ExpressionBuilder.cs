using Python.Model;

namespace Python.AST.Map.Test.Helpers.Models
{
    public static class ExpressionBuilder
    {
        private const int DefaultLineNumber = 1;

        public static Identifier BuildIdentifier(string[] identifierParts)
        {
            var identifier = new Identifier();
            identifier.SetId(identifierParts);
            return identifier;
        }

        public static Identifier BuildIdentifier(string[] identifierParts, string instancePointer)
        {
            var identifier = BuildIdentifier(identifierParts);
            identifier.SetInstancePointer(instancePointer);
            return identifier;
        }

        public static LocalVariableDeclaration BuildLocalVariable(Identifier identifier)
        {
            var localVariable = new LocalVariableDeclaration(DefaultLineNumber);
            localVariable.SetIdentifier(identifier);
            return localVariable;
        }

        public static ParameterDeclaration BuildParameterDeclaration(string name)
        {
            var parameter = new ParameterDeclaration(DefaultLineNumber);
            parameter.SetName(name);
            return parameter;
        }

        public static FieldDeclaration BuildStaticFieldDeclaration(Identifier identifier)
        {
            var field = new FieldDeclaration(DefaultLineNumber);
            field.SetIdentifier(identifier);
            field.SetAsStaticField();
            return field;
        }

        public static FieldDeclaration BuildInstanceFieldDeclaration(Identifier identifier)
        {
            var field = new FieldDeclaration(DefaultLineNumber);
            field.SetIdentifier(identifier);
            field.SetAsInstanceField();
            return field;
        }
    }
}