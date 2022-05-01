using Hosting.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modularization;
using System;
using System.Linq;
using System.Windows;
using System.Reactive.Linq;
using UIFramework;
using ReactiveUI;
using Microsoft.EntityFrameworkCore;
using UserViewer;
using DependencyInjection;

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
			var dbContexts = Container.GetServices<DbContext>().ToList();
			dbContexts.ForEach(context => context.Database.Migrate());

			var featureVm = Container.GetService<UserViewerViewModel>();

			var mainWindow = Container.GetService<MainWindowViewModel>();

			mainWindow.MainContentViewModel = featureVm;

			MainWindow = (Window)Container.GetService<IViewFor<MainWindowViewModel>>();
			((ReactiveWindow<MainWindowViewModel>)MainWindow).ViewModel = mainWindow;

			MainWindow.Show();
		}

		private static void BootstrapContainer(HostBuilderContext cx, IServiceCollection services)
		{
			services.AddSingleton(typeof(IServiceScopeFactory<>), typeof(ServiceScopeFactory<>));

			services.AddSingleton<MainWindowViewModel>();
			services.AddSingleton<IViewFor<MainWindowViewModel>, MainWindow>();

			services.ConfigureUI();

			services.RegisterServices()
				.GetAwaiter()
				.GetResult();
		}
	}
}
