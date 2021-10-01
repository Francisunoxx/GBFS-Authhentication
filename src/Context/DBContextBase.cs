using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AuthenticationApi
{
    public class DatabaseContext : DbContext
    {
        private readonly ILoggerFactory _logger;
        public DatabaseContext(DbContextOptions options, ILoggerFactory logger) :
            base(options)
        {
            _logger = logger;
            ChangeTracker.AutoDetectChangesEnabled = false;
            Database.SetCommandTimeout(300);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_logger);
        }

        public DbSet<User> Usr { get; set; }
        public DbSet<User> Rle { get; set; }
        public DbSet<RefreshToken> RefreshTkn { get; set; }
        public DbSet<UserPassword> UsrPassword { get; set; }
        public DbSet<Borrower> Brrwr { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder
                .Entity(User.EntityBuilder)
                .Entity(UserPassword.EntityBuilder)
                .Entity(Role.EntityBuilder)
                .Entity(RefreshToken.EntityBuilder)
                .Entity(Borrower.EntityBuilder);
    }

    public class MyContext : DatabaseContext
    {
        public MyContext(DbContextOptions<MyContext> options, ILoggerFactory logger) :
            base(options, logger)
        {
        }
    }
    
}