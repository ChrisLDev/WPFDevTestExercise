using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace UserViewer
{
    /// <summary>
    /// Interaction logic for CreateUserView.xaml
    /// </summary>
    public partial class CreateUserView : ICreateUserView
    {
        public CreateUserView()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            //Events
            Cancel.Events().Click.Subscribe(_ =>
            {
                DialogResult = false;
            });

            Create.Events().Click.Subscribe(_ =>
            {
                DialogResult = true;
            });

            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel,
                vm => vm.DateOfBirth,
                v => v.DateOfBirthPicker.SelectedDate).DisposeWith(disposables);

                this.Bind(ViewModel,
               vm => vm.Name,
               v => v.NameTxt.Text).DisposeWith(disposables);

                this.Bind(ViewModel,
                vm => vm.Profession,
                v => v.ProfessionTxt.Text).DisposeWith(disposables);

                this.OneWayBind(ViewModel,
               vm => vm.ButtonName,
               v => v.Create.Content).DisposeWith(disposables);

                this.OneWayBind(ViewModel,
               vm => vm.Title,
               v => v.Title.Text).DisposeWith(disposables);


            });
        }
        [Reactive] public string ButtonName { get; set; }

        public DateTime DateOfBirth => DateOfBirthPicker.SelectedDate ?? default;

        public string Profession => ProfessionTxt.Text;

        public string Name => NameTxt.Text;

		public Guid Id => ViewModel.Id;
	}
}
