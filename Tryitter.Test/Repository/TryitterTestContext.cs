namespace Tryitter.Test.Repository;

using Microsoft.EntityFrameworkCore;
using Tryitter.Web.Models;
using Microsoft.Extensions.DependencyInjection;
using Tryitter.Web.Repository;


public class TryitterTestContext : DatabaseContext
{
    public DbSet<User> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        //usamos a função UseInMemoryDatabase() para indicar que usaremos um banco de dados utilizando a memória interna 
        optionsBuilder.UseInMemoryDatabase("User").UseInternalServiceProvider(serviceProvider);
        optionsBuilder.UseInMemoryDatabase("Post").UseInternalServiceProvider(serviceProvider);
    }

}