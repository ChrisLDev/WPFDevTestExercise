using AutoMapper;
using Extensions;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using UIFramework;
using UIFramework.Abstrations;
using UIFramework.Abstrations.Theme;
using UserInfoFramework;
using UserInfoFramework.Models;

namespace UserViewer
{
	public class UserViewerViewModel : ViewModelBase
	{
		private readonly IUserInfoService _userInfoService;
		private readonly IMapper _mapper;
		public readonly IThemesController _themesController;

		public UserViewerViewModel(
			IUserInfoService userInfoService,
			IThemesController themesController,
			IMapper mapper)
		{
			_mapper = mapper;
			_userInfoService = userInfoService;
			_themesController = themesController;
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
				.Subscribe(user => UserDetials = _mapper.Map<UserDetialsViewModel>(user))
				.DisposeWith(disposables);

            AvailableThemes = _themesController.AvailableThemes.ToList();

            this.WhenAnyValue(x => x.SelectedTheme)
                .Subscribe(theme => _themesController.CurrentTheme = theme)
                .DisposeWith(disposables);


            base.OnActivation(disposables);
		}

		private async Task<IEnumerable<UserViewModel>> GetUsers() =>
			await _mapper.MapAsync<List<UserViewModel>, IEnumerable<User>>(_userInfoService.GetAllUsers());
	
		private async Task RefreshUsers()
		{
			Users = await GetUsers();
			DisplayUsers = Users.ToList();
			SelectedUser = null;
			UserDetials = null;
		}

		private IEnumerable<UserViewModel> Users { get; set; } = new List<UserViewModel>();

		[Reactive] public List<UserViewModel> DisplayUsers { get; set; }

		[Reactive] public UserViewModel SelectedUser { get; set; }

		[Reactive] public UserDetialsViewModel UserDetials {
			get; set; }

		public Interaction<(string buttonName, User user), (User User, bool IsConfirmed)> UserInteraction { get; set; } =
			new(RxApp.MainThreadScheduler);

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
				var result = await UserInteraction.Handle(("Create",null));

                if (result.IsConfirmed)
                {
					await _userInfoService.CreateUser(result.User);
					await RefreshUsers();
				}
			});

		public ReactiveCommand<UserViewModel, Unit> UpdateCommand =>
			ReactiveCommand.CreateFromTask<UserViewModel>(async user =>
			{
				var userModel = _mapper.Map<User>(user);

				var result = await UserInteraction.Handle(("Edit", userModel));

				if (result.IsConfirmed)
				{
					await _userInfoService.UpdateUser(result.User);
					await RefreshUsers();
				}
			});

		[Reactive] public List<ThemeResourceDictionary> AvailableThemes { get; set; }

		[Reactive] public ThemeResourceDictionary SelectedTheme { get; set; }
	}
}
