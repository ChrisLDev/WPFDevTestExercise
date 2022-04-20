using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.EntityFramework
{
    public abstract class GeneralDbContext<TContext>
        : DbContext where TContext : DbContext
    {
        protected GeneralDbContext() {}

        protected string MigrationAssembly => typeof(TContext).Assembly.FullName;

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=localhost;Database=WPFTestApp;User Id=Admin;Password=Password123;",
                x => x.MigrationsAssembly(MigrationAssembly));
        }
    }
}
