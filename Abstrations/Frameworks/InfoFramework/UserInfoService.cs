using AutoMapper;
using DataAccess.EntityFramework;
using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInfoFramework.Data;
using UserInfoFramework.Models;

namespace UserInfoFramework
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IRepository<UserInfoEntity> _repository;
        private readonly IMapper _mapper;

        public UserInfoService(IRepository<UserInfoEntity> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
           
        public Task<IEnumerable<User>> GetAllUsers() => 
            _mapper.MapAsync<IEnumerable<User>, UserInfoEntity[]>(_repository.QueryAsync());

        public Task<User> GetUserInfo(Guid Id) => 
            _mapper.MapAsync<User, UserInfoEntity>(_repository.GetAsync(x => x.Id == Id));

        public Task<bool> UpdateUser(User user) => _repository.UpdateAsync(_mapper.Map<UserInfoEntity>(user));

        public Task<bool> DeleteUser(User user) => _repository.DeleteAsync(_mapper.Map<UserInfoEntity>(user));

        public Task<bool> CreateUser(User user) => _repository.CreateAsync(_mapper.Map<UserInfoEntity>(user));
    }
}
