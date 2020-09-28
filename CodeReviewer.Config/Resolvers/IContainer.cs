using System;
using System.Collections.Generic;

namespace CodeReviewer.Config.Resolvers
{
    public interface IContainer
    {
        T Resolve<T>();

        T Resolve<T>(Func<T, bool> selector);

        T Resolve<T>(string objectName);

        T TryResolve<T>();

        IEnumerable<T> ResolveAll<T>();

        void Release(object instance);

        void Dispose();
    }
}