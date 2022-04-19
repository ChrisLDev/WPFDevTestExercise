using Microsoft.Extensions.DependencyInjection;
using Modularization;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIFramework.Abstrations;

namespace UIFramework
{
    public class MainWindowViewModel : ViewModelBase
    {
        [Reactive] public ReactiveObject MainContentViewModel { get; set; }

    }

    public class test : IModule
    {
        public void RegisterServices(IServiceCollection service)
        {
            throw new NotImplementedException();
        }
    }
}
