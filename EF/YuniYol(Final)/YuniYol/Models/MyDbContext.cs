using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace YuniYol.Models;

internal class MyDbContext : DbContext
{
    public DbSet<Users> Users {  get; set; }
    public DbSet<Administrators> Administrators { get; set; }
    public DbSet<Moderators> Moderators { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<Storage> Storages { get; set; }

    public MyDbContext()
    {
        
    }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json");

        var confiq = builder.Build();

        optionsBuilder.UseSqlServer(confiq.GetConnectionString("Default"));

    }
}
