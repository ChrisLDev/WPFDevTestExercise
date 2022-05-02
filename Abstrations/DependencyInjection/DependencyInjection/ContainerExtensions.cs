using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public static class ContainerExtensions
    {
        public static void AddFactory<TService, TImplementation>(this IServiceCollection services) 
            where TService : class 
            where TImplementation : class, TService
        {
            services.AddTransient<TService, TImplementation>();
            services.AddSingleton<Func<TService>>(x => x.GetService<TService>);
            services.AddSingleton<IFactory<TService>, Factory<TService>>();
        }

        public static void AddFactory<TService>(this IServiceCollection services)
           where TService : class
        {
            AddFactory<TService, TService>(services);
        }
    }
}
