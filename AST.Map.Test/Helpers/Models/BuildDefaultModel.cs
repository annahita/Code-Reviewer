using System.Collections.Generic;
using Python.Model;

namespace Python.AST.Map.Test.Helpers.Models
{
    public class BuildDefaultModel
    {
        private const string LocalVariableId = "DedualtLocalVariable";
        private const string ClassStaticFieldId = "DedualtClassStaticField";
        private const string ClassInstanceFieldId = "DedualtClassInstanceField";
        private const string ParameterName = "DedualtParameter";
        private const string InstancePointer = "self";
        private const string MethodName = "DefualtMethod";
        private const string EmptyMethodName = "DefualtEmptyMethod";
        private const string ClassName = "DefualtClass";

        private ClassDeclaration _classDeclaration;
        private MethodDeclaration _methodDeclaration;
        private MethodDeclaration _methodDeclarationEmptyBody;

        public BuildDefaultModel()
        {
            BuildDefaultClass();
        }

        public ClassDeclaration GetClass()
        {
            return _classDeclaration;
        }

        public MethodDeclaration GetMethod()
        {
            return _methodDeclaration;
        }

        public MethodDeclaration GetMethodWithEmptyBody()
        {
            return _methodDeclarationEmptyBody;
        }

        public void BuildDefaultClass()
        {
            _classDeclaration = ClassBuilder.BuildMethod(ClassName);
            _classDeclaration.SetFields(BuildFields());
            BuildMethod();
            BuildMethodWithEmptyBody();
            _classDeclaration.SetMethods(new[] {_methodDeclaration, _methodDeclarationEmptyBody});
        }

        private IEnumerable<FieldDeclaration> BuildFields()
        {
            var fields = new List<FieldDeclaration>
            {
                ExpressionBuilder.BuildStaticFieldDeclaration(
                    ExpressionBuilder.BuildIdentifier(new[] {ClassStaticFieldId})),
                ExpressionBuilder.BuildInstanceFieldDeclaration(
                    ExpressionBuilder.BuildIdentifier(new[] {ClassInstanceFieldId}))
            };
            return fields;
        }

        private void BuildMethodWithEmptyBody()
        {
            var parameter = ExpressionBuilder.BuildParameterDeclaration(ParameterName);
            var instancePointer = ExpressionBuilder.BuildParameterDeclaration(InstancePointer);
            _methodDeclarationEmptyBody =
                MethodBuilder.BuildMethod(EmptyMethodName, new[] {parameter}, instancePointer);
            _methodDeclarationEmptyBody.SetParentClass(_classDeclaration);
        }

        private void BuildMethod()
        {
            var parameter = ExpressionBuilder.BuildParameterDeclaration(ParameterName);
            var instancePointer = ExpressionBuilder.BuildParameterDeclaration(InstancePointer);

            _methodDeclaration = MethodBuilder.BuildMethod(MethodName, new[] {parameter}, instancePointer);
            _methodDeclaration.SetMethodBody(BuildMethodBody());
            _methodDeclaration.SetParentClass(_classDeclaration);
        }

        private MethodBody BuildMethodBody()
        {
            var localVariable =
                ExpressionBuilder.BuildLocalVariable(ExpressionBuilder.BuildIdentifier(new[] {LocalVariableId}));
            var assignStatement =
                StatementBuilder.BuildAssignStatement(new[] {localVariable}, _classDeclaration.Fields);
            return MethodBuilder.BuildMethodBody(new[] {assignStatement});
        }
    }
}