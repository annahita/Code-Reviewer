namespace Python.AST.Map.RelationResolver
{
    internal enum ExpressionType
    {
        AccessedMember,
        UndefinedField,
        UndefinedClassInstanceField,
        UndefinedClassStaticField,
        UndefinedLocalVariable
    }
}