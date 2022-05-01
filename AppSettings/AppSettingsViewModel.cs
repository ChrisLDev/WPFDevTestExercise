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
        public readonly IThemesController _themesController;

        public AppSettingsViewModel(IThemesController themesController)
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

        public MainWindowViewModel MainWindowViewModel { get; set; }

        public ReactiveCommand<Unit, Unit> CloseSettingsCommand => ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.IsSettingsActive = false;
        });

        [Reactive] public List<ThemeResourceDictionary> AvailableThemes { get; set; }

        [Reactive] public ThemeResourceDictionary SelectedTheme { get; set; }
    }
}
