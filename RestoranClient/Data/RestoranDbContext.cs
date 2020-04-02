using Microsoft.EntityFrameworkCore;
using RestoranClient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestoranClient.Data
{
    public class RestoranDbContext:DbContext
    {
        public DbSet<Waiter>  Waiters { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Abonent> Abonent { get; set; }
        public DbSet<SourceItem> Sources { get; set; }
        public DbSet<FoodItem> Items { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(Config.Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        }
    }
}
