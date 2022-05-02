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
    public class LicensesViewModel : ViewModelBase, ISettingsItem
    {

        public LicensesViewModel()
        {


        }

        protected override void OnActivation(CompositeDisposable disposables)
        {
          
        }

        public string MenuCategory => "03_Licenses";

        public string IconResource => "LicensesIcon";

        public string MenuItemName => "Licenses";

        public Type ContentViewModelType => typeof(LicensesViewModel);

    }
}
