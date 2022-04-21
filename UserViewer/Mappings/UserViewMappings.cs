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

			CreateMap<UserDetialsViewModel, UserViewModel>().ReverseMap()
			.ConstructUsingServiceLocator();

			CreateMap<UserDetialsViewModel, User>().ReverseMap()
			.ConstructUsingServiceLocator();
		}
	}
}
