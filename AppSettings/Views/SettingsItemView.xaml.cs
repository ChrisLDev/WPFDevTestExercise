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
    /// Interaction logic for SettingsItemView.xaml
    /// </summary>
    public partial class SettingsItemView
    {
        public SettingsItemView()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                ViewModel.WhenAnyValue(x => x.SettingsItem).WhereNotNull().Subscribe(x =>
                {
                    if (x.IconResource != null)
                    {
                        ItemButton.Tag = TryFindResource(x.IconResource) as Style;
                    }
                    ItemButton.Tag ??= TryFindResource("SettingsIcon") as Style;

                }).DisposeWith(disposables);

                this.OneWayBind(ViewModel,
                    vm => vm.SettingsItem.MenuItemName,
                    v => v.ItemButton.Content)
                .DisposeWith(disposables);

                this.Bind(ViewModel,
                    vm => vm.IsSelected,
                    v => v.ItemButton.IsChecked)
                .DisposeWith(disposables);
            });
        }
    }
}
