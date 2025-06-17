using System.Collections.Generic;
using System;

namespace Assets.CodeBase.Infrastructure.ServiceLocator
{
    public class AllServices
    {
        private static AllServices instance;
        public static AllServices Container => instance ?? (instance = new AllServices());

        private readonly Dictionary<Type, IService> services = new Dictionary<Type, IService>();

        // Ограничение: TypeService является IService
        public void RegisterSingle<TypeService>(TypeService implementation) where TypeService : class, IService
        {
            services.Add(typeof(TypeService), implementation);
        }

        public void UnregisterSingle<TypeService>() where TypeService : class, IService
        {
            services.Remove(typeof(TypeService));
        }

        public TypeService Single<TypeService>() where TypeService : class, IService
        {
            if (services.ContainsKey(typeof(TypeService)))
            {
                return services[typeof(TypeService)] as TypeService;
            }

            return null;
        }
    }
}