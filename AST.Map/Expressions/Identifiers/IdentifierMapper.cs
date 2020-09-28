using System.Collections.Generic;
using System.Linq;
using Python.Antlr.ElementFinders;
using Python.AST.Map.RelationResolver;
using Python.Model;

namespace Python.AST.Map.Expressions.Identifiers
{
    internal class IdentifierMapper : IAstMapper<Identifier>
    {
        private const short InstancePointerPosition = 1;
        private static readonly string NullInstancePointer = string.Empty;
        private readonly IIdentifierElementFinder _elementFinder;
        private readonly IParentReferenceResolver _parent;
        private Identifier _identifier;

        internal IdentifierMapper(IIdentifierElementFinder elementFinder, IParentReferenceResolver parent)

        {
            _elementFinder = elementFinder;
            _parent = parent;
            MapElements();
        }

        public Identifier GetMappedItem()
        {
            return _identifier;
        }

        private void MapElements()
        {
            ICollection<string> identifierNames = _elementFinder.FindIdentifier().ToList();
            _identifier = new Identifier();
            _identifier.SetInstancePointer(GetInstancePointer(identifierNames));
            _identifier.SetId(GetId(identifierNames));
        }

        private string GetInstancePointer(ICollection<string> identifierNames)
        {
            return _parent.IsInstancePointer(identifierNames.FirstOrDefault())
                ? identifierNames.FirstOrDefault()
                : NullInstancePointer;
        }

        private IEnumerable<string> GetId(IEnumerable<string> identifierNames)
        {
            return _identifier.HasInstancePointer() ? identifierNames.Skip(InstancePointerPosition) : identifierNames;
        }
    }
}