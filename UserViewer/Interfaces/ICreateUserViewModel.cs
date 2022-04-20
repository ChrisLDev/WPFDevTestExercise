using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserViewer
{
    public interface ICreateUserViewModel : IReactiveObject
    {
        string Name { get; set; }

        DateTime DateOfBirth { get; set; }

        string Profession { get; set; }
    }
}
