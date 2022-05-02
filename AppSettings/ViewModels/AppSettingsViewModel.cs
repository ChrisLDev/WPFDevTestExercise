using AppSettings.Interfaces;
using DependencyInjection;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using UIFramework;
using UIFramework.Abstrations;
using UIFramework.Abstrations.Theme;

namespace AppSettings
{
    public class AppSettingsViewModel : ViewModelBase
    {
        public AppSettingsViewModel(
            IEnumerable<ISettingsItem> settingsItems,
            IFactory<SettingsItemViewModel> settingsFactory
            )
        {
            SettingsItems = settingsItems
                .OrderBy(x => x.MenuCategory)
                .Select((feature, index) =>
                {
                    var item = settingsFactory.Create();
                    item.Host = this;
                    item.SettingsItem = feature;
                    item.Index = index;
                    return item;
                }).ToList();

            CurrentItem = SettingsItems.First().SettingsItem as ReactiveObject;
        }

        protected override void OnActivation(CompositeDisposable disposables)
        {
           
        }

        public MainWindowViewModel MainWindowViewModel { get; set; }

        public ReactiveCommand<Unit, Unit> CloseSettingsCommand => ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.IsSettingsActive = false;
        });

       [Reactive] public List<SettingsItemViewModel> SettingsItems { get; set; }

        [Reactive] public ReactiveObject CurrentItem { get; set; }
    }
}
