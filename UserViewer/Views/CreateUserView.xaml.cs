using ReactiveUI;
using System;
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

        }

        public DateTime DateOfBirth => DateOfBirthPicker.SelectedDate ?? default;
        public string Profession => ProfessionTxt.Text;
        public string Name => NameTxt.Text;
    }
}
