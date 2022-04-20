using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserViewer
{
    public interface ICreateUserDialogFactory
    {
        ICreateUserView Create();
    }
}
