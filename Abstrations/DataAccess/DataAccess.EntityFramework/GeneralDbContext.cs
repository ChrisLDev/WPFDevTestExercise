using Hosting.CommonObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DataAccess.EntityFramework
{
    public abstract class GeneralDbContext<TContext>
        : DbContext where TContext : DbContext
    {
        private readonly string connectionString;

		protected GeneralDbContext(IOptions<ClientConfiguration> configuration)
		{   
            connectionString = configuration.Value.ConnectionString;
		}

		protected string MigrationAssembly => typeof(TContext).Assembly.FullName;

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(connectionString,
                x => x.MigrationsAssembly(MigrationAssembly));
        }
    }
}
