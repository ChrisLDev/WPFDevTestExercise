using DataAccess.EntityFramework;
using Hosting.CommonObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInfoFramework.Data
{
    public class UserInfoDbContext 
        : GeneralDbContext<UserInfoDbContext>
    {
        public UserInfoDbContext(IOptions<ClientConfiguration> configuration) : base (configuration) {}

        public DbSet<UserInfoEntity> Users { get; set; }
    }
}
