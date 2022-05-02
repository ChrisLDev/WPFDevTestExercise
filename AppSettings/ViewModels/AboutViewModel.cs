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
    public class AboutViewModel : ViewModelBase, ISettingsItem
    {

        public AboutViewModel()
        {


        }

        protected override void OnActivation(CompositeDisposable disposables)
        {
          
        }

        public string MenuCategory => "04_About";

        public string IconResource => "InfoIcon";

        public string MenuItemName => "About";

        public Type ContentViewModelType => typeof(AboutViewModel);

    }
}
