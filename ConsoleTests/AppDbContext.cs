using AngelStack.DomainDrivenDesign.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConsoleTests;

public class AppDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureGeographicEntities();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var connectionString = "Server=localhost;Database=ddd_tests;User Id=sa;Password=sqlserver;Trusted_Connection=True;TrustServerCertificate=True";

        optionsBuilder.UseSqlServer(connectionString);
        //optionsBuilder.UseSnakeCaseNamingConvention();
    }
}
