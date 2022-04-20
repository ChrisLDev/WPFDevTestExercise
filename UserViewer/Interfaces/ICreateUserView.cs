using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserViewer
{
    public interface ICreateUserView : IViewFor<ICreateUserViewModel>
    {
        string Name { get; }

        DateTime DateOfBirth { get; }

        string Profession { get; }

        bool? ShowDialog();
    }
}
