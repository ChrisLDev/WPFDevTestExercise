using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserViewer
{
	/// <summary>
	/// Interaction logic for UserViewerView.xaml
	/// </summary>
	public partial class UserViewerView
	{
		public UserViewerView()
		{
			InitializeComponent();

			this.WhenActivated(disposables =>
			{
				this.WhenAnyValue(x => x.ViewModel)
					.WhereNotNull()
					.Subscribe(vm => DataContext = vm)
					.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
					vm => vm.DispalyUsers,
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
					v => v.EditPanel.ViewModel)
				.DisposeWith(disposables);
			});
		}
	}
}
