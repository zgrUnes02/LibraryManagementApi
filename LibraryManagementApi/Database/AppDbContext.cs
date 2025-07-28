using LibraryManagementApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) 
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Author>().ToTable("authors");
            modelBuilder.Entity<Book>().ToTable("books");

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Book>()
                .HasIndex(b => new
                {
                    b.Name,
                    b.Isbn
                })
                .IsUnique();

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = 1,
                        FirstName = "admin",
                        LastName = "admin",
                        Email = "admin@gmail.com",
                        Password = "AQAAAAIAAYagAAAAEIDi0PUC5W4yIOsGi1B0JmiR9HlYQ1mYHpoHZj8dqJlkEn8B0d8McW/7IZsA3I6hbw==",
                        Role = "Admin"
                    }
                );
        }
    }
}
