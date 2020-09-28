using System.Collections.Generic;
using System.Linq;
using OOP_Model;
using Python.Model;
using Python.Model.Extension;

namespace Python.AST.Map.RelationResolver
{
    internal class MethodReferenceResolver : IParentReferenceResolver
    {
        private readonly Dictionary<string, IExpression> _declaredExpressions = new Dictionary<string, IExpression>();
        private readonly string _instancePointer;
        private readonly bool _methodHasInstancePointer;
        private readonly ClassReferenceResolver _parentClass;

        internal MethodReferenceResolver(MethodDeclaration method)
        {
            _parentClass = new ClassReferenceResolver(method.ParentClass);
            AddMethodMembersToExpressions(method);
            _instancePointer = method.HasInstancePointer() ? method.InstancePointer.Name : string.Empty;
            _methodHasInstancePointer = method.HasInstancePointer();
        }

        public IExpression FindMember(Identifier id)
        {
            return id.HasInstancePointer()
                ? _parentClass.FindMember(id)
                : SearchInDeclaredExpressions(id);
        }

        public ExpressionType ResolveType(Identifier id)
        {
            return Exist(id) ? ExpressionType.AccessedMember : ResolveUndefinedType(id);
        }

        public bool IsInstancePointer(string id)
        {
            return _methodHasInstancePointer && id.Equals(_instancePointer);
        }

        private void AddMethodMembersToExpressions(MethodDeclaration method)
        {
            foreach (var parameter in method.Parameters) _declaredExpressions.Add(parameter.Name, parameter);
        }

        private bool Exist(Identifier id)
        {
            return !(FindMember(id) is NullExpression);
        }

        private IExpression SearchInDeclaredExpressions(Identifier id)
        {
            return _declaredExpressions.ContainsKey(id.GetTarget())
                ? _declaredExpressions[id.GetTarget()]
                : new NullExpression();
        }

        private ExpressionType ResolveUndefinedType(Identifier id)
        {
            if (IsSingleVariable(id))
                return ExpressionType.UndefinedLocalVariable;
            return IsNewClassInstanceField(id)
                ? ExpressionType.UndefinedClassInstanceField
                : ExpressionType.UndefinedField;
        }

        private bool IsSingleVariable(Identifier id)
        {
            return id.Id.Count() == 1 && !id.HasInstancePointer();
        }

        private bool IsNewClassInstanceField(Identifier id)
        {
            return id.Id.Count() == 1 && id.HasInstancePointer();
        }

        internal void Update(IEnumerable<IExpression> expressions)
        {
            AddNewExpression(expressions.FilterExpression<LocalVariableDeclaration>());
            _parentClass.Update(expressions);
        }

        private void AddNewExpression(IEnumerable<LocalVariableDeclaration> variables)
        {
            foreach (var variable in variables) _declaredExpressions.Add(variable.Identifier.GetField(), variable);
        }
    }
}