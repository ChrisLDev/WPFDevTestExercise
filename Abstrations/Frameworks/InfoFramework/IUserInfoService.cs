using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInfoFramework.Models;

namespace UserInfoFramework
{
    public interface IUserInfoService
    {
        Task<User> GetUserInfo(Guid Id);

        Task<IEnumerable<User>> GetAllUsers();

        Task<bool> UpdateUser(User user);

        Task<bool> DeleteUser(User user);
    }
}
