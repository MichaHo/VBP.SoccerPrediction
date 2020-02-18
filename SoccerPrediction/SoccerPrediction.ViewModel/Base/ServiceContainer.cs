using System;
using System.Collections.Generic;

namespace SoccerPrediction.ViewModel
{
    public class ServiceContainer
    {
        public static readonly ServiceContainer ServiceInstance = new ServiceContainer();

        static readonly Dictionary<Type, object> ServiceMap;
        static readonly object ServiceMapLock;
        static ServiceContainer()
        {
            ServiceMap = new Dictionary<Type, object>();
            ServiceMapLock = new object();
        }

        public void AddService<TServiceContract>(TServiceContract implementation) where TServiceContract : class
        {
            lock (ServiceMapLock) ServiceMap[typeof(TServiceContract)] = implementation;
        }

        public static TServiceContract GetService<TServiceContract>() where TServiceContract : class
        {
            object service = null;
            lock (ServiceMapLock) ServiceMap.TryGetValue(typeof(TServiceContract), out service);
            return service as TServiceContract;
        }
    }
}
