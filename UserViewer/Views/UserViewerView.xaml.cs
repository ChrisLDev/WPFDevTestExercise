using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Windows;
using UserInfoFramework.Models;
using System.Windows.Controls.Primitives;

namespace UserViewer
{
	/// <summary>
	/// Interaction logic for UserViewerView.xaml
	/// </summary>
	public partial class UserViewerView
	{
		public UserViewerView(IServiceProvider service)
		{
			InitializeComponent();

			this.WhenActivated(disposables =>
			{
				this.WhenAnyValue(x => x.ViewModel)
					.WhereNotNull()
					.Subscribe(vm => DataContext = vm)
					.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
					vm => vm.DisplayUsers,
					v => v.UsersList.ItemsSource)
				.DisposeWith(disposables);

				this.Bind(ViewModel,
					vm => vm.Search,
					v => v.Search.Text)
				.DisposeWith(disposables);

				this.Bind(ViewModel,
					vm => vm.SelectedUser,
					v => v.UsersList.SelectedItem)
				.DisposeWith(disposables);

				this.Bind(ViewModel,
					vm => vm.EditableUser,
					v => v.EditPanelViewModel.ViewModel)
				.DisposeWith(disposables);

				this.BindCommand(ViewModel,
					vm => vm.CreateUserCommand,
					v => v.AddNewUserBtn)
				.DisposeWith(disposables);

				this.WhenAnyValue(x => x.ViewModel.EditableUser)
				.Subscribe(user =>
				{
					EditPanel.Visibility = user == null 
					? Visibility.Collapsed 
					: Visibility.Visible;
                }).DisposeWith(disposables);

				UpdateUser
				.Events().Click
				.Subscribe(_ =>
				{
					ViewModel.UpdateCommand
						.Execute(ViewModel.EditableUser)
						.Subscribe();
				});

				//Interactions
				ViewModel.CreateUserInteraction.RegisterHandler(interaction =>
				{
					var factory = service.GetService<ICreateUserDialogFactory>();
					var dialog = factory.Create();
					var result = dialog.ShowDialog() ?? false;

					//TODO Make dialog return user
					var user = new User
					{
						Name = dialog.Name,
						DateOfBirth = dialog.DateOfBirth,
						Profession = dialog.Profession,
					};

					interaction.SetOutput((User: user, IsConfirmed: result));
				});
			});
		}
	}
}
