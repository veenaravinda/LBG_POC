using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Mortgage> Mortgages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<Property>().HasKey(c => c.Id);

            // Additional configurations...
            //modelBuilder.Entity<Mortgage>()
            //    .HasOne(p => p.CustomerEntitys)
            //    .WithMany()
            //    .HasForeignKey(p => p.CustomerId);

            //modelBuilder.Entity<Mortgage>()
            //    .HasOne(p => p.PropertyEntitys)
            //    .WithMany()
            //    .HasForeignKey(p => p.PropertyId);

            base.OnModelCreating(modelBuilder);

        }

    }
}
