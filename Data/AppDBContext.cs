using api_rest_cs.Models;
using Microsoft.EntityFrameworkCore;

namespace api_rest_cs.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public required DbSet<Book> Books { get; set; }
    public required DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasOne(b => b.User)
            .WithMany(u => u.Books)
            .HasForeignKey(b => b.UserId);
    }
}
