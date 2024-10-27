using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace DbSeeder;

public class CompanyDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Complaint> Complaints { get; set; }
    public DbSet<ReturnEmployees> ReturnEmployees { get; set; }
    public DbSet<Return> Returns { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=data.db");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var dbSeeder = new DbSeeder();
        dbSeeder.Seed(10000);
        
        modelBuilder.Entity<Product>().HasData(dbSeeder.Products);
        modelBuilder.Entity<Employee>().HasData(dbSeeder.Employees);
        modelBuilder.Entity<Complaint>().HasData(dbSeeder.Complaints);
        modelBuilder.Entity<ReturnEmployees>().HasData(dbSeeder.ReturnEmployees);
        modelBuilder.Entity<Return>().HasData(dbSeeder.Returns);
        
        base.OnModelCreating(modelBuilder);
    }
}