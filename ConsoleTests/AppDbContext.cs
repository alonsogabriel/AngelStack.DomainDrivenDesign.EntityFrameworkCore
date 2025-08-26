using AngelStack.DomainDrivenDesign.EntityFrameworkCore;
using AngelStack.DomainDrivenDesign.EntityFrameworkCore.Mappings;
using ConsoleTests.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleTests;

public class AppDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .ConfigureGeographicEntities()
            .ApplyConfiguration(new PersonMap())
            .ApplyConfiguration(new UserMap());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var connectionString = "Server=localhost;Database=ddd_tests;User Id=sa;Password=sqlserver;Trusted_Connection=True;TrustServerCertificate=True";

        optionsBuilder.UseSqlServer(connectionString);
        //optionsBuilder.UseSnakeCaseNamingConvention();
    }
}
