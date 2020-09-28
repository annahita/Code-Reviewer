using System.Collections.Generic;
using Python.Antlr.ElementFinders;
using Python.Model;

namespace Python.AST.Map.Methods
{
    internal class StaticMethodMapper : MethodMapperBase
    {
        internal StaticMethodMapper(ClassDeclaration parent, IMethodElementFinder elementFinder) : base(parent,
            elementFinder)
        {
        }

        protected override void MapSpacialElements()
        {
            Method.SetAsStaticMethod();
        }

        protected override IEnumerable<IParameterElementFinder> FindMethodParameters()
        {
            return ElementFinder.FindParameters();
        }
    }
}