using System.Collections.Generic;
using System.Linq;
using OOP_Model;
using Python.Antlr.ElementFinders;
using Python.AST.Map.RelationResolver;
using Python.AST.Map.Statements;
using Python.Model;

namespace Python.AST.Map.Methods
{
    public class MethodBodyMapper : IAstMapper<MethodBody>
    {
        private readonly IMethodBodyElementFinder _elementFinder;
        private readonly MethodDeclaration _parentMethod;

        private readonly MethodReferenceResolver _referenceResolver;
        private MethodBody _methodMethodBody;

        public MethodBodyMapper(IMethodBodyElementFinder elementFinder, MethodDeclaration method)
        {
            _elementFinder = elementFinder;
            _parentMethod = method;
            _referenceResolver = new MethodReferenceResolver(method);
            MapElements();
        }

        public MethodBody GetMappedItem()
        {
            return _methodMethodBody;
        }

        private void MapElements()
        {
            _methodMethodBody =
                new MethodBody(_elementFinder.GetLineNumber(), _elementFinder.GetEndLineNumber(),
                    GetCountsOfDirectStatements());
            _methodMethodBody.SetParent(_parentMethod);
            _methodMethodBody.SetEmbeddedStatements(MapEmbeddedStatements());
            _methodMethodBody.SetSimpleStatements(MapSimpleStatements());
        }

        private IEnumerable<ISimpleStatement> MapSimpleStatements()
        {
            return MapAssignStatements().Concat(MapReturnStatements().Concat(MapSingleStatements()));
        }

        private IEnumerable<EmbeddedStatement> MapEmbeddedStatements()
        {
            var embeddedStatements = _elementFinder.FindEmbeddedStatements();
            return embeddedStatements.Select(t =>
                new EmbeddedStatementMapper(t).GetMappedItem());
        }


        private IEnumerable<ISimpleStatement> MapSingleStatements()
        {
            var singleStatements = _elementFinder.FindSingleStatements();
            return singleStatements.Select(t =>
                new SinglePartStatementMapper(t, _referenceResolver).GetMappedItem());
        }

        private IEnumerable<ISimpleStatement> MapReturnStatements()
        {
            var retStatements = _elementFinder.FindReturnStatements();
            return retStatements.Select(t =>
                new SinglePartStatementMapper(t, _referenceResolver).GetMappedItem());
        }

        private IEnumerable<ISimpleStatement> MapAssignStatements()
        {
            var assignStatements = _elementFinder.FindAssignStatements();

            return assignStatements.Select(MapAssignStatement);
        }

        private AssignStatement MapAssignStatement(IAssignStatementElementFinder assignStatement)
        {
            var statement = new AssignStatementMapper(assignStatement, _referenceResolver)
                .GetMappedItem();
            _referenceResolver.Update(statement.Expressions);
            return statement;
        }

        private int GetCountsOfDirectStatements()
        {
            return _elementFinder.CountOfDirectStatementsInBody();
        }
    }
}