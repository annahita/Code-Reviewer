using System.Collections.Generic;
using System.Linq;
using Python.Antlr.ElementFinders;
using Python.Model;

namespace Python.AST.Map.Statements
{
    public class EmbeddedStatementMapper : IAstMapper<EmbeddedStatement>
    {
        private readonly IEmbeddedStatementElementFinder _elementFinder;
        private EmbeddedStatement _structure;

        public EmbeddedStatementMapper(IEmbeddedStatementElementFinder elementFinder)
        {
            _elementFinder = elementFinder;

            MapElements();
        }

        public EmbeddedStatement GetMappedItem()
        {
            return _structure;
        }

        private void MapElements()
        {
            _structure = new EmbeddedStatement
                (_elementFinder.GetLineNumber(), GetCountsOfDirectStatements());
            _structure.SetEmbedded_structure(ResolveInternalEmbeddedStatements());
        }

        private IEnumerable<EmbeddedStatement> ResolveInternalEmbeddedStatements()
        {
            var statements = _elementFinder.FindNestedEmbeddedStatements();
            return statements.Select(MapNestedStructure);
        }

        private EmbeddedStatement MapNestedStructure(IEmbeddedStatementElementFinder embeddedStatementElementFinder)
        {
            var embeddedMapper = new EmbeddedStatementMapper(embeddedStatementElementFinder);
            return embeddedMapper.GetMappedItem();
        }

        private int GetCountsOfDirectStatements()
        {
            return _elementFinder.CountOfDirectStatementsInBody();
        }
    }
}