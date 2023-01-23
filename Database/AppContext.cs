using Microsoft.EntityFrameworkCore;


namespace Database
{
    public class AppContext : DbContext
    {
        public DbSet<Database.Models.User> Users { get; set; }
        public DbSet<Database.Models.Session> Sessions { get; set; }
        public DbSet<Database.Models.Doctor> Doctors { get; set; }
        public DbSet<Database.Models.Schedule> Schedules { get; set; }
        public DbSet<Database.Models.Specialization> Specializations { get; set; }

        public AppContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Database.Models.User>().HasIndex(model => model.UserName);
        }
    }
}
