using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIFramework.Abstrations;

namespace UserViewer
{
    public class CreateUserViewModel : ViewModelBase, ICreateUserViewModel
    {
        [Reactive] public string Name { get; set; }
        [Reactive] public DateTime DateOfBirth { get; set; }
        [Reactive] public string Profession { get; set; }
		public Guid Id { get; set; }

        [Reactive] public string ButtonName { get; set; }

        [Reactive] public string Title { get; set; }
    }
}
