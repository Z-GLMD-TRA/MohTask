using Microsoft.EntityFrameworkCore;
using MohTask.Infrastructure.Users;
using MohTask.Model.Users;

namespace MohTask.Infrastructure
{
    public class MohTaskDbContext : DbContext
    {

        public MohTaskDbContext()
        {

        }
        public MohTaskDbContext(DbContextOptions<MohTaskDbContext> dbContext) : base(dbContext)
        {

        }
        /// <summary>
        /// user table in DB
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Configures the database connection if not already configured.
        /// </summary>

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MohTastDB;TrustServerCertificate=True;Integrated Security=SSPI");
            }

        }
        /// <summary>
        /// load and add models properties and relations
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureUserModelBuilder();
        }
    }

}
