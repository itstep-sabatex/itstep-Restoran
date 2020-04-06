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
        public DbSet<FoodItem> FoodItems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(Config.Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Abonent>(entity =>
            {
                entity.ToTable("abonent");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<ClientCards>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });
            modelBuilder.Entity<FoodItem>(entity =>
            {
                entity.ToTable("items");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(18, 2)");
            });


            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(p => p.WaiterId).HasColumnName("waiter_id");
                entity.Property(p => p.AbonentId).HasColumnName("abonent_id");
                entity.Property(p => p.SourceId).HasColumnName("source_id");
                entity.Property(p => p.TimeOrder).HasColumnName("time_order");
                entity.Property(p => p.EndOrder).HasColumnName("end_order");
            });
            modelBuilder.Entity<Detail>(entity =>
            {
                entity.Property(p => p.OrderId).HasColumnName("order_id");
                entity.Property(p => p.ItemsId).HasColumnName("items_id");
                entity.Property(p => p.Bill).HasColumnName("bill");
                entity.Property(p => p.Count).HasColumnName("count");
                entity.Property(p => p.Price).HasColumnName("price");
            });

        }

    }
}
