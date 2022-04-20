using Microsoft.Extensions.DependencyInjection;
using Modularization;
using ReactiveUI;

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

			service.AddTransient<EditUserViewModel>();
			service.AddTransient<IViewFor<EditUserViewModel>, EditUserView>();

			service.AddAutoMapper(typeof(UserViewMappings));
		}
	}
}
