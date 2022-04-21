using ReactiveUI.Fody.Helpers;
using System;
using UIFramework.Abstrations;

namespace UserViewer
{
	public class UserDetialsViewModel : ViewModelBase
	{
		public Guid Id { get; set; }

		[Reactive] public string Name { get; set; }

		[Reactive] public DateTime DateOfBirth { get; set; }

		[Reactive] public string Profession { get; set; }
	}
}
