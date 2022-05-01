using Microsoft.Extensions.DependencyInjection;
using Modularization;
using ReactiveUI;

namespace AppSettings
{
    public class AppSettingsModule : IModule
    {
        public void RegisterServices(IServiceCollection service)
        {
            service.AddTransient<AppSettingsViewModel>();
            service.AddTransient<IViewFor<AppSettingsViewModel>, AppSettingsView>();
        }
    }
}
