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
        Guid Id { get; set; }
        string Name { get; set; }

        DateTime DateOfBirth { get; set; }

        string Profession { get; set; }

        string ButtonName { get; set; }
        string Title { get; set; }
    }
}
