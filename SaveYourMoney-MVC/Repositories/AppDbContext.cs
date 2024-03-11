using System;
namespace SaveYourMoney_MVC.Repositories
{

    using Microsoft.EntityFrameworkCore;
    using SaveYourMoney_MVC.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Budget> Budgets { get; set; }

    }

}

