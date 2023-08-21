using Jt.Common.Tool.DI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jt.Common.Tool.Extension
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 服务接口注入
        /// </summary>
        /// <param name="services">services</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddServiceByJtDIInterface(this IServiceCollection services)
        {
            services.RegisterLifetimesByInhreit(typeof(IScopedDIInterface));
            services.RegisterLifetimesByInhreit(typeof(ITransientDIInterface));
            services.RegisterLifetimesByInhreit(typeof(ISingletonDIInterface));
            return services;
        }

        private static void RegisterLifetimesByInhreit(this IServiceCollection services, Type lifetimeType)
        {
            List<Type> types = AppDomain.CurrentDomain
                                .GetAssemblies()
                                .SelectMany(x => x.GetTypes())
                                .Where(x => lifetimeType.IsAssignableFrom(x) && x.IsClass && !x.IsAbstract)
                                .ToList();
            foreach (Type type in types)
            {
                var interfaces = type.GetInterfaces().Where(x => x != lifetimeType).ToList();
                if (interfaces.Count > 0)
                {
                    foreach (var item in interfaces)
                    {
                        if (lifetimeType == typeof(ISingletonDIInterface))
                        {
                            services.AddSingleton(item, type);
                        }
                        else if (lifetimeType == typeof(IScopedDIInterface))
                        {
                            services.AddScoped(item, type);
                        }
                        else if (lifetimeType == typeof(ITransientDIInterface))
                        {
                            services.AddTransient(item, type);
                        }
                    }
                }
                else
                {
                    if (lifetimeType == typeof(ISingletonDIInterface))
                    {
                        services.AddSingleton(type);
                    }
                    else if (lifetimeType == typeof(IScopedDIInterface))
                    {
                        services.AddScoped(type);
                    }
                    else if (lifetimeType == typeof(ITransientDIInterface))
                    {
                        services.AddTransient(type);
                    }
                }
            }
        }
    }
}
