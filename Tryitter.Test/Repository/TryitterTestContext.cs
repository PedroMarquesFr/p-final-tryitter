namespace Tryitter.Test.Repository;

using Microsoft.EntityFrameworkCore;
using Tryitter.Web.Models;
using Microsoft.Extensions.DependencyInjection;
using Tryitter.Web.Repository;


public class TryitterTestContext : DatabaseContext
{
    public DbSet<User> User1 { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        //usamos a função UseInMemoryDatabase() para indicar que usaremos um banco de dados utilizando a memória interna 
        optionsBuilder.UseInMemoryDatabase("User1").UseInternalServiceProvider(serviceProvider);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
          .HasKey(i => i.PostId);
        modelBuilder.Entity<User>()
          .HasKey(i => i.UserId);

        modelBuilder.Entity<Post>()
          .HasOne(i => i.User)
          .WithMany(i => i.Posts)
          .HasForeignKey(b => b.UserId)
          .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
          .HasMany(i => i.Posts)
          .WithOne(i => i.User)
          .HasForeignKey(b => b.PostId)
          .OnDelete(DeleteBehavior.Cascade);
    }

}