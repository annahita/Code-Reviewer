using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Windsor;

namespace CodeReviewer.Config.Resolvers
{
    public class Container : IContainer, IDisposable
    {
        private readonly IWindsorContainer _windsorContainer;

        public Container(IWindsorContainer windsorContainer)
        {
            _windsorContainer = windsorContainer;
        }

        public T Resolve<T>()
        {
            return _windsorContainer.Resolve<T>();
        }

        public T Resolve<T>(Func<T, bool> selector)
        {
            var allInstances = _windsorContainer.ResolveAll<T>();
            return allInstances.First(selector);
        }

        public T Resolve<T>(string objectName)
        {
            return _windsorContainer.Resolve<T>(objectName);
        }

        public T TryResolve<T>()
        {
            var instances = _windsorContainer.ResolveAll<T>();
            return instances.Any() ? instances.First() : default;
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return _windsorContainer.ResolveAll<T>();
        }

        public void Release(object instance)
        {
            _windsorContainer.Release(instance);
        }

        public void Dispose()
        {
            _windsorContainer.Dispose();
        }
    }
}