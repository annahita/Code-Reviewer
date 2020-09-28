using System.Collections.Generic;
using System.Linq;
using OOP_Model;
using Python.Model;
using Python.Model.Extension;

namespace Python.AST.Map.RelationResolver
{
    internal class ClassReferenceResolver : IParentReferenceResolver
    {
        private readonly IDictionary<string, IExpression> _declaredExpressions = new Dictionary<string, IExpression>();

        internal ClassReferenceResolver(ClassDeclaration classDeclaration)
        {
            foreach (var field in classDeclaration.Fields) _declaredExpressions.Add(field.Identifier.GetField(), field);
        }

        public IExpression FindMember(Identifier id)
        {
            return _declaredExpressions.ContainsKey(id.GetTarget())
                ? _declaredExpressions[id.GetTarget()]
                : new NullExpression();
        }

        public ExpressionType ResolveType(Identifier id)
        {
            return Exist(id) ? ExpressionType.AccessedMember : ResolveUndefinedType(id);
        }

        public bool IsInstancePointer(string id)
        {
            return false;
        }

        private ExpressionType ResolveUndefinedType(Identifier id)
        {
            return IsSingleVariable(id) ? ExpressionType.UndefinedClassStaticField : ExpressionType.UndefinedField;
        }

        private bool IsSingleVariable(Identifier id)
        {
            return id.Id.Count() == 1;
        }

        private bool Exist(Identifier id)
        {
            return !(FindMember(id) is NullExpression);
        }

        internal void Update(IEnumerable<IExpression> expressions)
        {
            AddNewExpression(expressions.FilterExpression<FieldDeclaration>());
        }

        private void AddNewExpression(IEnumerable<FieldDeclaration> fields)
        {
            foreach (var field in fields) _declaredExpressions.Add(field.Identifier.GetField(), field);
        }
    }
}