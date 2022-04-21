using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Windows;

namespace UserViewer
{
	/// <summary>
	/// Interaction logic for UserView.xaml
	/// </summary>
	public partial class UserView
	{
		public UserView()
		{
			InitializeComponent();

			this.WhenActivated(disposables =>
			{
				this.WhenAnyValue(x => x.ViewModel)
					.WhereNotNull()
					.Subscribe(vm => DataContext = vm)
					.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
					vm => vm.Name,
					v => v.Name.Text)
				.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
					vm => vm.DateOfBirth,
					v => v.DateOfBirth.Text,
					x => x.ToString("dd/MM/yyyy"))
				.DisposeWith(disposables);
			});

		}
	}
}
