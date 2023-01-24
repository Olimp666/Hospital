using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database
{
    public class AppContextFactory : IDesignTimeDbContextFactory<AppContext>
    {
        public AppContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppContext>();
            optionsBuilder.UseNpgsql(
                $"Host=localhost;Port=5432;Database=backend_db;Username=postgres;Password=niggers");

            return new AppContext(optionsBuilder.Options);
        }
    }
}
