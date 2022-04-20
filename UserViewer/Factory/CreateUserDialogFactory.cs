using Microsoft.Extensions.DependencyInjection;
using System;

namespace UserViewer.Factory
{
    public class CreateUserDialogFactory : ICreateUserDialogFactory
    {
        private readonly IServiceProvider _service;

        public CreateUserDialogFactory(IServiceProvider service)
        {
            _service = service;
        }   

        public ICreateUserView Create()
        {
            var Iview = _service.GetService<ICreateUserView>();
            var Ivm = _service.GetService<ICreateUserViewModel>();

            Iview.ViewModel = Ivm;

            return Iview;
        }
    }
}
