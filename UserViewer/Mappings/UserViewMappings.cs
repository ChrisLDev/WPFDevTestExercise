using AutoMapper;
using UserInfoFramework.Models;

namespace UserViewer
{
	public class UserViewMappings : Profile
	{
		public UserViewMappings()
		{
			CreateMap<UserViewModel, User>();
			CreateMap<User, UserViewModel>()
				.ConstructUsingServiceLocator();

			CreateMap<EditUserViewModel, UserViewModel>().ReverseMap()
			.ConstructUsingServiceLocator();

			CreateMap<EditUserViewModel, User>().ReverseMap()
			.ConstructUsingServiceLocator();
		}
	}
}
