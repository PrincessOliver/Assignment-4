﻿using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class NorthwindContex : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder
            .LogTo(Console.Out.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        optionsBuilder.UseNpgsql("host=localhost;db=northwind;uid=postgres;pwd=*");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().ToTable("categories");
        modelBuilder.Entity<Category>()
            .Property(x => x.Id).HasColumnName("categoryid");
        modelBuilder.Entity<Category>()
            .Property(x => x.Name).HasColumnName("categoryname");
        modelBuilder.Entity<Category>()
            .Property(x => x.Description).HasColumnName("description");


        modelBuilder.Entity<Product>().ToTable("products");
        modelBuilder.Entity<Product>().Property(x => x.Id).HasColumnName("productid");
        modelBuilder.Entity<Product>().Property(x => x.Name).HasColumnName("productname");
        modelBuilder.Entity<Product>().Property(x => x.CategoryId).HasColumnName("categoryid");
        modelBuilder.Entity<Product>().Property(x => x.QuantityPerUnit).HasColumnName("quantityperunit");
        modelBuilder.Entity<Product>().Property(x => x.UnitsInStock).HasColumnName("unitsinstock");
        modelBuilder.Entity<Product>().Property(x => x.UnitPrice).HasColumnName("unitprice");
    }
}
