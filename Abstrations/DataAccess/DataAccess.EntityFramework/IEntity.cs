using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework
{
    public interface IEntity
    {
        /// <summary>
        /// The unique identifier of the entity assigned by the ORM
        /// </summary>
        public Guid Id { get; set; }
    }
}
