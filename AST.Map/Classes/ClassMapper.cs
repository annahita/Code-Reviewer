using System.Collections.Generic;
using System.Linq;
using Python.Antlr.ElementFinders;
using Python.AST.Map.Methods;
using Python.AST.Map.RelationResolver;
using Python.AST.Map.Statements;
using Python.Model;
using Python.Model.Extension;

namespace Python.AST.Map.Classes
{
    internal class ClassMapper : IAstMapper<ClassDeclaration>
    {
        private readonly IClassElementFinder _elementFinder;
        private ClassDeclaration _classDeclaration;

        internal ClassMapper(IClassElementFinder elementFinder)
        {
            _elementFinder = elementFinder;
            MapElements();
        }

        public ClassDeclaration GetMappedItem()
        {
            return _classDeclaration;
        }

        private void MapElements()
        {
            _classDeclaration = new ClassDeclaration(_elementFinder.GetLineNumber());
            _classDeclaration.SetName(MapClassName());
            _classDeclaration.SetSuperClasses(MapSuperClasses());
            _classDeclaration.SetConstructors(MapClassConstructors());
            _classDeclaration.SetMethods(MapMethods());
            _classDeclaration.SetFields(MapFields());
        }

        private string MapClassName()
        {
            return _elementFinder.FindName();
        }

        private IEnumerable<string> MapSuperClasses()
        {
            return _elementFinder.FindSuperClasses();
        }

        private IEnumerable<MethodDeclaration> MapClassConstructors()
        {
            var constructorTrees = _elementFinder.FindConstructors();
            return constructorTrees.Select(tree => new InstanceMethodMapper(_classDeclaration, tree).GetMappedItem());
        }

        #region Fields Mapper

        private IEnumerable<FieldDeclaration> MapFields()
        {
            var fields = new List<FieldDeclaration>();
            fields.AddRange(MapInstanceFields());
            fields.AddRange(MapStaticFields());
            return fields;
        }

        private IEnumerable<FieldDeclaration> MapInstanceFields()
        {
            var fields = new List<FieldDeclaration>();
            foreach (var item in _classDeclaration.Constructors)
            {
                var constructorBody = (MethodBody) item.MethodBody;
                fields.AddRange(constructorBody.GetDeclaredFields().Where(a => a.IsInstanceField));
            }

            return fields;
        }

        private IEnumerable<FieldDeclaration> MapStaticFields()
        {
            var assignments = MapAssignStatements();
            return assignments.SelectMany(a => a.Expressions).FilterExpression<FieldDeclaration>();
        }

        private IEnumerable<AssignStatement> MapAssignStatements()
        {
            var assignStatements = _elementFinder.FindAssignStatements();
            var resolver = new ClassReferenceResolver(_classDeclaration);
            return assignStatements.Select(statement => MapAssignStatement(statement, resolver));
        }

        private static AssignStatement MapAssignStatement(IAssignStatementElementFinder assignStatement,
            ClassReferenceResolver resolver)
        {
            var statement = new AssignStatementMapper(assignStatement, resolver).GetMappedItem();
            resolver.Update(statement.Expressions);
            return statement;
        }

        #endregion

        #region Methods Mapper

        private IEnumerable<MethodDeclaration> MapMethods()
        {
            return MapStaticMethods().Concat(MapInstanceMethods());
        }

        private IEnumerable<MethodDeclaration> MapStaticMethods()
        {
            var methodTrees = _elementFinder.FindStaticMethods();
            return methodTrees.Select(methodTree =>
                new StaticMethodMapper(_classDeclaration, methodTree).GetMappedItem());
        }

        private IEnumerable<MethodDeclaration> MapInstanceMethods()
        {
            var methodTrees = _elementFinder.FindInstanceMethods();
            return methodTrees.Select(methodTree =>
                new InstanceMethodMapper(_classDeclaration, methodTree).GetMappedItem());
        }

        #endregion
    }
}