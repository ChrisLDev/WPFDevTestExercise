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
	/// Interaction logic for UserDetialsView.xaml
	/// </summary>
	public partial class UserDetialsView
	{
		public UserDetialsView()
		{
			InitializeComponent();

			this.WhenActivated(disposables =>
			{
				this.OneWayBind(ViewModel,
					vm => vm.Name,
					v => v.Name.Text)
				.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
					vm => vm.Profession,
					v => v.Profession.Text)
				.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
					vm => vm.DateOfBirth,
					v => v.Date.Text)
				.DisposeWith(disposables);
			});
		}
	}
}
