using AutoMapper;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Reactive;
using UIFramework.Abstrations;
using UserInfoFramework;
using UserInfoFramework.Models;

namespace UserViewer
{
	public class EditUserViewModel : ViewModelBase
	{
		public Guid Id { get; set; }

		[Reactive] public string Name { get; set; }

		[Reactive] public DateTime DateOfBirth { get; set; }

		[Reactive] public string Profession { get; set; }
	}
}
