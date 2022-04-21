using Microsoft.Extensions.DependencyInjection;
using System;
using UserInfoFramework.Models;

namespace UserViewer.Factory
{
    public class CreateUserDialogFactory : ICreateUserDialogFactory
    {
        private readonly IServiceProvider _service;

        public CreateUserDialogFactory(IServiceProvider service)
        {
            _service = service;
        }   

        public ICreateUserView Create(string buttonActionName, User user)
        {
            var Iview = _service.GetService<ICreateUserView>();
            var Ivm = _service.GetService<ICreateUserViewModel>();

			if (user != null)
			{
                Ivm.Name = user.Name;
                Ivm.Profession = user.Profession;
                Ivm.DateOfBirth = user.DateOfBirth;
                Ivm.Id = user.Id;
            }

            Ivm.ButtonName = buttonActionName;
            Ivm.Title = buttonActionName + " User";

            Iview.ViewModel = Ivm;

            return Iview;
        }
    }
}
