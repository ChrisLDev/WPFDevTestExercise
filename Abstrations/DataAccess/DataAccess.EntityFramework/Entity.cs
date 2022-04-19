using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework
{
    public class Entity : IEntity
    {
        /// <inheritdoc cref="IEntity.Id"/>
        public Guid Id { get; set; }
    }
}
