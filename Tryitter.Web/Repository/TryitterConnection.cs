using Microsoft.EntityFrameworkCore;
using Tryitter.Web.Models;

namespace Tryitter.Web.Repository;
public class DatabaseContext : DbContext
{
    public DbSet<User>? User { get; set; }
    public DbSet<Post>? Post { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=127.0.0.1;Database=tryitter;User=SA;Password=SenhaSegura123.;");
    }
}