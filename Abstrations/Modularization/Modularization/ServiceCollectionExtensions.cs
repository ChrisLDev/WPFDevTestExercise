using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace Modularization
{
    public static class ServiceCollectionExtensions
    {
        public static IObservable<Unit> RegisterServices(this IServiceCollection services)
        {
            return Observable.FromAsync(async () =>
            {
                
                var assemblies = AssemblyLoadContext.Default.Assemblies.ToList();

                foreach (var assembly in assemblies)
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (type.GetInterface(nameof(IModule)) == typeof(IModule))
                        {
                            var ggg = true;
                        }

                        if (type.GetInterface(nameof(IModule)) == typeof(IModule) && Activator.CreateInstance(type) is IModule module)
                        {
                            module.RegisterServices(services);
                        }
                    }
                }
            });
        }
    }
}
