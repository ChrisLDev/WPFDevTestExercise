using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserInfoFramework.Models;

namespace UserInfoFramework
{
    public interface IUserInfoService
    {
        /// <summary>
        /// Get User from the repository
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<User> GetUserInfo(Guid Id);

        /// <summary>
        /// Get all users from the reposoitory
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<User>> GetAllUsers();

        /// <summary>
        /// Create a new user entry in the repository
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> CreateUser(User user);

        /// <summary>
        /// Update an exisiting user in the repository,
        /// the id of the user will ref the updated user entity
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> UpdateUser(User user);

        /// <summary>
        /// Deletes the user from the repository
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> DeleteUser(User user);
    }
}
