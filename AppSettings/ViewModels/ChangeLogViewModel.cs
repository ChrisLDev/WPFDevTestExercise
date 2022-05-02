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
    public class ChangeLogViewModel : ViewModelBase, ISettingsItem
    {

        public ChangeLogViewModel()
        {


        }

        protected override void OnActivation(CompositeDisposable disposables)
        {
          
        }

        public string MenuCategory => "02_ChangeLog";

        public string IconResource => "ChangelogIcon";

        public string MenuItemName => "ChangeLog";

        public Type ContentViewModelType => typeof(ChangeLogViewModel);

    }
}
