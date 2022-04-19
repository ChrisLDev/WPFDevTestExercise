using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.EntityFramework
{
    public abstract class GeneralDbContext<TContext>
        : DbContext where TContext : DbContext
    {
        private readonly ILogger<TContext> _logger;

        protected GeneralDbContext(
            DbContextOptions<TContext> options,
            ILogger<TContext> logger) : base (options) =>
            _logger = logger;

        protected string MigrationAssembly => typeof(TContext).Assembly.FullName;

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=localhost;Database=WPFTestApp;User=Admin;Password=Password123;",
                x => x.MigrationsAssembly(MigrationAssembly));
        }
    }
}
