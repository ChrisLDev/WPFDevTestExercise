using AutoMapper;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using UIFramework.Abstrations;
using UserInfoFramework;
using UserInfoFramework.Models;

namespace UserViewer
{
	public class UserViewerViewModel : ViewModelBase
	{
		private readonly IUserInfoService _userInfoService;
		private readonly IMapper _mapper;

		public UserViewerViewModel(
			IUserInfoService userInfoService,
			IMapper mapper)
		{
			_mapper = mapper;
			_userInfoService = userInfoService;
		}

		protected override void OnActivation(CompositeDisposable disposables)
		{
			Observable.StartAsync(async () => await RefreshUsers());

			this.WhenAnyValue(x => x.Search)
				.Throttle(TimeSpan.FromMilliseconds(200))
				.ObserveOnDispatcher()
				.WhereNotNull()
				.Subscribe(searchfilter => DisplayUsers = Users
					.Where(x => x.Name
						.Contains(searchfilter, StringComparison.CurrentCultureIgnoreCase))
					.ToList())
				.DisposeWith(disposables);

			this.WhenAnyValue(x => x.SelectedUser)
				.WhereNotNull()
				.Subscribe(user => EditableUser = _mapper.Map<EditUserViewModel>(user))
				.DisposeWith(disposables);

			base.OnActivation(disposables);
		}

		private async Task<IEnumerable<UserViewModel>> GetUsers()
		{
			var users = await _userInfoService.GetAllUsers();

			return _mapper.Map<List<UserViewModel>>(users);
		}

		private async Task RefreshUsers()
		{
			Users = await GetUsers();
			DisplayUsers = Users.ToList();
		}

		private IEnumerable<UserViewModel> Users { get; set; } = new List<UserViewModel>();

		[Reactive] public List<UserViewModel> DisplayUsers { get; set; }

		[Reactive] public UserViewModel SelectedUser { get; set; }

		[Reactive] public EditUserViewModel EditableUser { get; set; }

		public Interaction<Unit, (User User, bool IsConfirmed)> CreateUserInteraction { get; set; } = new Interaction<Unit, (User, bool)>(RxApp.MainThreadScheduler);

		[Reactive] public string Search { get; set; }

		public ReactiveCommand<UserViewModel, Unit> RemoveUserCommand =>
			ReactiveCommand.CreateFromTask<UserViewModel>(async user =>
			{
				var userModel = _mapper.Map<User>(user);

				await _userInfoService.DeleteUser(userModel);

				await RefreshUsers();
			});

		public ReactiveCommand<Unit, Unit> CreateUserCommand =>
			ReactiveCommand.CreateFromTask(async () =>
			{
				var result = await CreateUserInteraction.Handle(Unit.Default);

                if (result.IsConfirmed)
                {
					await _userInfoService.CreateUser(result.User);
					await RefreshUsers();
				}	
			});

		public ReactiveCommand<EditUserViewModel, Unit> UpdateCommand =>
			ReactiveCommand.CreateFromTask<EditUserViewModel>(async user =>
			{
				var userModel = _mapper.Map<User>(user);

				await _userInfoService.UpdateUser(userModel);

				await RefreshUsers();
			});
	}
}
