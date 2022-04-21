using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInfoFramework.Models;

namespace UserViewer
{
    public interface ICreateUserDialogFactory
    {
        ICreateUserView Create(string buttonActionName, User user);
    }
}
