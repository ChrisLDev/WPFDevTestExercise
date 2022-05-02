using AppSettings.Interfaces;
using DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Modularization;
using ReactiveUI;

namespace AppSettings
{
    public class AppSettingsModule : IModule
    {
        public void RegisterServices(IServiceCollection service)
        {
            service.AddTransient<IViewFor<SettingsItemViewModel>, SettingsItemView>();

            service.AddTransient<ThemeSettingsViewModel>();
            service.AddTransient<ISettingsItem, ThemeSettingsViewModel>();
            service.AddTransient<IViewFor<ThemeSettingsViewModel>, ThemeSettingsView>();

            service.AddTransient<AboutViewModel>();
            service.AddTransient<ISettingsItem, AboutViewModel>();
            service.AddTransient<IViewFor<AboutViewModel>, AboutView>();

            service.AddTransient<ChangeLogViewModel>();
            service.AddTransient<ISettingsItem, ChangeLogViewModel>();
            service.AddTransient<IViewFor<ChangeLogViewModel>, ChangeLogView>();

            service.AddTransient<LicensesViewModel>();
            service.AddTransient<ISettingsItem, LicensesViewModel>();
            service.AddTransient<IViewFor<LicensesViewModel>, LicensesView>();

            service.AddFactory<SettingsItemViewModel, SettingsItemViewModel>();

            service.AddTransient<AppSettingsViewModel>();
            service.AddTransient<IViewFor<AppSettingsViewModel>, AppSettingsView>();
        }
    }
}
