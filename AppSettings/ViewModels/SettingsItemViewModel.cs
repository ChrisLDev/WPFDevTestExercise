using AppSettings.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using UIFramework.Abstrations;

namespace AppSettings
{
    public class SettingsItemViewModel : ViewModelBase
    {
        private ReactiveObject _settingsItemViewModel;
        private readonly IServiceProvider _serviceProvider;

        public SettingsItemViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override void OnActivation(CompositeDisposable disposables)
        {
            this.WhenAnyValue(x => x.IsSelected)
                .Where(x => x)
                .Subscribe(x =>
                {
                    _settingsItemViewModel ??= _serviceProvider.GetService(SettingsItem.ContentViewModelType) as ReactiveObject;
                    Host.CurrentItem = _settingsItemViewModel;
                }).DisposeWith(disposables);

            base.OnActivation(disposables);
        }

        public AppSettingsViewModel Host { get; set; }

        public ISettingsItem SettingsItem { get; set; }

        /// <summary>
        /// The Index of the item within the menu
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// A selection flag for the item
        /// </summary>
        [Reactive] public bool IsSelected { get; set; }
    }
}
