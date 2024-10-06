using APIFlashCard.Models;
using Microsoft.EntityFrameworkCore;

namespace APIFlashCard.Data
{
    public class FlashCardDbContext : DbContext
    {
        public FlashCardDbContext(DbContextOptions<FlashCardDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<EncryptionKeys> EncryptionKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasKey(c => c.ID_Category);

            modelBuilder.Entity<User>()
                .ToTable("user")
                .HasKey(u => u.ID_User);

            modelBuilder.Entity<UserDetails>()
                .HasKey(ud => ud.ID_Detailed);

            modelBuilder.Entity<EncryptionKeys>()
                .HasKey(ek => ek.ID);

            modelBuilder.Entity<Category>()
                .ToTable("category");
                //.HasOne(c => c.User)
                //.WithMany(u => u.Categories)
                //.HasForeignKey(c => c.UserID);

            modelBuilder.Entity<UserDetails>()
                .HasOne(ud => ud.User)
                .WithMany(u => u.UserDetails)
                .HasForeignKey(ud => ud.ID_User);

            modelBuilder.Entity<EncryptionKeys>()
                .HasOne(ek => ek.User)
                .WithMany(u => u.EncryptionKeys)
                .HasForeignKey(ek => ek.ID_User);
        }
    }
}

