namespace CodeReviewer.Config.Resolvers
{
    public static class DependencyResolver
    {
        public static IContainer Current { get; private set; }

        public static void Initial(IContainer container)
        {
            if (Current == null) Current = container;
        }
    }
}