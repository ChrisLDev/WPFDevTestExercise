using Microsoft.Extensions.DependencyInjection;
using Modularization;
using ReactiveUI;
using UserViewer.Factory;

namespace UserViewer
{
	public class UserViewerModule : IModule
	{
		public void RegisterServices(IServiceCollection service)
		{
			service.AddTransient<UserViewerViewModel>();
			service.AddTransient<IViewFor<UserViewerViewModel>, UserViewerView>();

			service.AddTransient<UserViewModel>();
			service.AddTransient<IViewFor<UserViewModel>, UserView>();

			service.AddTransient<UserDetialsViewModel>();
			service.AddTransient<IViewFor<UserDetialsViewModel>, UserDetialsView>();

			service.AddTransient<ICreateUserView, CreateUserView>();
			service.AddTransient<ICreateUserViewModel, CreateUserViewModel>();
			service.AddSingleton<ICreateUserDialogFactory, CreateUserDialogFactory>();

			service.AddAutoMapper(typeof(UserViewMappings));
		}
	}
}
