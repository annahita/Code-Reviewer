using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using CodeReviewer.Config.Resolvers;
using CodeReviewer.Config.WireUp;

namespace CodeReviewer.Config
{
    public static class Configurator
    {
        public static void WireUp()
        {
            var container = new WindsorContainer();
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
            AstMapperBootstrapper.WireUp(container);
            CleanCodeBootstrapper.WireUp(container);
            Bootstrapper.WireUp(container);
            DependencyResolver.Initial(container.Resolve<IContainer>());
        }
    }
}