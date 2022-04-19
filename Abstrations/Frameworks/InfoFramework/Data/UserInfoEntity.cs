using DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInfoFramework.Data
{
    public class UserInfoEntity : Entity
    {
        public string Name { get; set; }
        public string Profession { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
