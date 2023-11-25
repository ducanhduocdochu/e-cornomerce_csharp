using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class DBAppContext : DbContext
    {
        public DBAppContext(DbContextOptions<DBAppContext> options)
            : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<KeyToken> KeyToken { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<RefreshTokenUsed> RefreshTokenUsed { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // 1-n: user - role
            // Convert enum to String
            // modelBuilder.Entity<User>()
            //     .Property(e => e.Status)
            //     .HasConversion(new StatusConverter());

            // Apikey
            // modelBuilder.Entity<User>()
            //     .HasMany(a => a.UserRole)
            //     .WithOne(b => b.User)
            //     .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<User>()
            .HasOne(u => u.KeyToken)
            .WithOne(k => k.User)
            .HasForeignKey<KeyToken>(k => k.user_id);

            modelBuilder.Entity<User>()
            .HasOne(u => u.UserInfo)
            .WithOne(k => k.User)
            .HasForeignKey<UserInfo>(k => k.user_id);

            modelBuilder.Entity<User>()
            .HasMany(u => u.RefreshTokensUsed)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.user_id);
        }
    }
}
