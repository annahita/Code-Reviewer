using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Python.Antlr;
using Python.AST.Map;

namespace CodeReviewer.Config.WireUp
{
    public static class AstMapperBootstrapper
    {
        public static void WireUp(IWindsorContainer container)
        {
            container.Register(Component.For<IParserBuilder>().ImplementedBy<ParserBuilder>().LifestyleTransient());
            container.Register(Component.For<IRootMapper>().ImplementedBy<RootMapper>().LifestyleTransient());
        }
    }
}