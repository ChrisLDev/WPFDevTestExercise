using AutoMapper;
using UserInfoFramework.Data;
using UserInfoFramework.Models;

namespace UserInfoFramework.Mappings
{
    public class UserServiceMappings : Profile
    {
        public UserServiceMappings()
        {
            CreateMap<UserInfoEntity, User>().ReverseMap();
        }
    }
}
