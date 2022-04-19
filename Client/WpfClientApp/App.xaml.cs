using Hosting.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Modularization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using System.Reactive.Linq;
using UIFramework;
using ReactiveUI;


namespace WpfClientApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Container = ReactiveServiceHost.Create(BootstrapContainer);
        }

        public IServiceProvider Container { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //var dbContexts = Container.GetServices<DbContext>().ToList();
           // dbContexts.ForEach(context => context.Database.Migrate());

            var mainWindow = Container.GetService<MainWindowViewModel>();

            MainWindow = (Window)Container.GetService<IViewFor<MainWindowViewModel>>();
            ((ReactiveWindow<MainWindowViewModel>)MainWindow).ViewModel = mainWindow;

            MainWindow.Show();
        }

        private static void BootstrapContainer(HostBuilderContext cx, IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<IViewFor<MainWindowViewModel>, MainWindow>();

            services.RegisterServices()
                .GetAwaiter()
                .GetResult();
        }
    }
}
