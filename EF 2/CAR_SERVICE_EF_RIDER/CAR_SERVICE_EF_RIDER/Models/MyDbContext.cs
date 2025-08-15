using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CAR_SERVICE_EF_RIDER.Models;

public class MyDbContext : DbContext
{
    public MyDbContext() {}

    public DbSet<User> Users { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Admin> Admins { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder(); // ConfigurationBuilder что делает?
        builder.AddJsonFile("appsettings.json"); // Он находит appsettings.json во время рантайма?

        var confiq = builder.Build();

        optionsBuilder.UseSqlServer(confiq.GetConnectionString("Default"));
        optionsBuilder.EnableSensitiveDataLogging();
    }
}