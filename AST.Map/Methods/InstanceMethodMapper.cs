using System.Collections.Generic;
using System.Linq;
using Python.Antlr.ElementFinders;
using Python.Model;

namespace Python.AST.Map.Methods
{
    internal class InstanceMethodMapper : MethodMapperBase
    {
        private const short InstancePointerPosition = 1;

        internal InstanceMethodMapper(ClassDeclaration parent, IMethodElementFinder elementFinder) : base(parent,
            elementFinder)
        {
        }

        protected override void MapSpacialElements()
        {
            Method.SetInstancePointer(MapInstancePointer());
        }

        protected override IEnumerable<IParameterElementFinder> FindMethodParameters()
        {
            return ElementFinder.FindParameters().Skip(InstancePointerPosition);
        }

        private ParameterDeclaration MapInstancePointer()
        {
            var instancePointer = FindInstancePointer();
            return new ParameterMapper(instancePointer).GetMappedItem();
        }

        private IParameterElementFinder FindInstancePointer()
        {
            return ElementFinder.FindParameters().Take(InstancePointerPosition).FirstOrDefault();
        }
    }
}