using AppSettings.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using UIFramework;
using UIFramework.Abstrations;
using UIFramework.Abstrations.Theme;

namespace AppSettings
{
    public class ThemeSettingsViewModel : ViewModelBase, ISettingsItem
    {
        private readonly IThemesController _themesController;

        public ThemeSettingsViewModel(IThemesController themesController)
        {
            _themesController = themesController;

            AvailableThemes = _themesController.AvailableThemes.ToList();
        }

        protected override void OnActivation(CompositeDisposable disposables)
        {
            SelectedTheme = _themesController.CurrentTheme;

            this.WhenAnyValue(x => x.SelectedTheme)
                .Subscribe(theme => _themesController.CurrentTheme = theme)
                .DisposeWith(disposables);
        }

        public string MenuCategory => "01_Theme";

        public string IconResource => "ThemeIcon";

        public string MenuItemName => "Theme";

        public Type ContentViewModelType => typeof(ThemeSettingsViewModel);

        [Reactive] public List<ThemeResourceDictionary> AvailableThemes { get; set; }

        [Reactive] public ThemeResourceDictionary SelectedTheme { get; set; }
    }
}
