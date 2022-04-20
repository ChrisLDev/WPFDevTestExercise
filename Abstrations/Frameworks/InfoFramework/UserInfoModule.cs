using DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modularization;
using UserInfoFramework.Data;
using UserInfoFramework.Mappings;

namespace UserInfoFramework
{
    public class UserInfoModule : IModule
    {
        public void RegisterServices(IServiceCollection service)
        {
            service.AddDbContext<UserInfoDbContext>();
            service.AddTransient<DbContext, UserInfoDbContext>();
            service.AddTransient<IRepository<UserInfoEntity>, GenericRepository<UserInfoDbContext, UserInfoEntity>>();

            service.AddTransient<IUserInfoService, UserInfoService>();

            service.AddAutoMapper(typeof(UserServiceMappings));
        }
    }
}
