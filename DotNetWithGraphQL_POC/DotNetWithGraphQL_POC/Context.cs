using Microsoft.EntityFrameworkCore;

namespace DotNetWithGraphQL_POC;

public class Context : DbContext
{
    public DbSet<Bee> Bees { get; set; }
    public DbSet<Hive> Hives { get; set; }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hive>().HasData(new Hive { Id = 1, Name = "Buzz" });
        modelBuilder.Entity<Bee>().HasData(new Bee { Id = 1, Age = 10, IsQueen = true, HiveId = 1 });
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(
            @"Server=(localdb)\mssqllocaldb;Database=BeeHives;Trusted_Connection=True").UseLazyLoadingProxies();
    }
}
