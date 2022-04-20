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
		private readonly IMapper _mapper;
		private readonly IUserInfoService _userInfoService;

		public EditUserViewModel(IMapper mapper, IUserInfoService userInfoService)
		{
			_mapper = mapper;
			_userInfoService = userInfoService;
		}

		public Guid Id { get; set; }

		[Reactive] public string Name { get; set; }

		[Reactive] public DateTime DateOfBirth { get; set; }

		[Reactive] public string Profession { get; set; }

		//public ReactiveCommand<Unit, Unit> SaveCommand =>
		//	ReactiveCommand.CreateFromTask(async () =>
		//	{
		//		var user = _mapper.Map<User>(this);

		//		await _userInfoService.UpdateUser(user);
		//	});

	}
}
