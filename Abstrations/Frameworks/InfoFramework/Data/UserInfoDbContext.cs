using DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
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
        public UserInfoDbContext() {}

        public DbSet<UserInfoEntity> Person { get; set; }
    }
}
