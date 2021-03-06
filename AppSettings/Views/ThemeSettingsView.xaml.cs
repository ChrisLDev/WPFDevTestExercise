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

namespace AppSettings
{
    /// <summary>
    /// Interaction logic for ThemeSettingsView.xaml
    /// </summary>
    public partial class ThemeSettingsView
    {
        public ThemeSettingsView()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel,
                    vm => vm.AvailableThemes,
                    v => v.Themes.ItemsSource)
                .DisposeWith(disposables);

                this.Bind(ViewModel,
                    vm => vm.SelectedTheme,
                    v => v.Themes.SelectedItem)
                .DisposeWith(disposables);
            });
        }
    }
}
