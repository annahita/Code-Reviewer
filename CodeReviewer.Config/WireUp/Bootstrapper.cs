using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CodeReviewer.Config.Resolvers;

namespace CodeReviewer.Config.WireUp
{
    public static class Bootstrapper
    {
        public static void WireUp(IWindsorContainer container)
        {
            container.Register(Component.For<IContainer>().ImplementedBy<Container>().UsingFactoryMethod(p =>
            {
                return new Container(container);
            }));
        }
    }
}