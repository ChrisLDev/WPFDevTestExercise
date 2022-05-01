using Microsoft.Extensions.DependencyInjection;
using Modularization;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using UIFramework.Abstrations;

namespace UIFramework
{
    public class MainWindowViewModel : ViewModelBase
    {
        [Reactive] public ReactiveObject MainContentViewModel { get; set; }

        [Reactive] public ReactiveObject SettingsViewModel { get; set; }

        [Reactive] public bool IsSettingsActive { get; set; }

        public ReactiveCommand<Unit, Unit> OpenSettingsCommand => ReactiveCommand.Create(() =>
        {
            IsSettingsActive = true;
        });
    }
}
