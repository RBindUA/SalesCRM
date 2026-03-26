using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sales.Domain.Entities;

namespace Sales.Infrastructure.Data
{
    public class AdventureWorksContext : DbContext
    {
        public AdventureWorksContext(DbContextOptions<AdventureWorksContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers {get; set;}
        public DbSet<SalesOrder> SalesOrders {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "Sales");
                entity.HasKey(e => e.CustomerId);

                entity.Ignore(c => c.FirstName);
                entity.Ignore(c => c.LastName);
                entity.Ignore(c => c.EmailAddress);
            });

            modelBuilder.Entity<SalesOrder>(entity =>
            {
                entity.ToTable("SalesOrderHeader", "Sales");
                entity.HasKey(e => e.SalesOrderId);
                entity.Property(e => e.SalesOrderId).HasColumnName("SalesOrderId");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
