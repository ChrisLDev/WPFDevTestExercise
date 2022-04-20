using Microsoft.Extensions.DependencyInjection;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Modularization
{
    public static class ServiceCollectionExtensions
    {
		public static IObservable<Unit> RegisterServices(this IServiceCollection services) =>
			Observable.FromAsync(async () =>
			{
				var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
				var loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();

				var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
				var toLoad = referencedPaths.Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase)).ToList();

				toLoad.ForEach(path => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path))));

				AssemblyLoadContext.Default.Assemblies
				.ToList()
				.ForEach(assembly => assembly.GetTypes()
					.ToList()
					.ForEach(type =>
					{
						if (type.GetInterface(nameof(IModule)) == typeof(IModule) &&
							Activator.CreateInstance(type) is IModule module)
						{
							module.RegisterServices(services);
						}
					}));
			});
	}
}
