using ReactiveUI;
using ReactiveUI.Validation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace UIFramework.Abstrations
{
    public abstract class ViewModelBase : ReactiveValidationObject, IActivatableViewModel
    {
        protected ViewModelBase()
        {
            Activator = new ViewModelActivator();

            this.WhenActivated(disposables =>
            {
                OnActivation(disposables);

                Disposable.Create(() => OnDeactivateion())
                    .DisposeWith(disposables);
            });
        }

        protected virtual void OnActivation(CompositeDisposable disposables) { }

        protected virtual void OnDeactivateion() { }

        public ViewModelActivator Activator { get; }
    }
}
