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
        public DbSet<Person> Persons { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Customer> Customers {get; set;}
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "Sales");
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Ignore(c => c.FirstName);
                entity.Ignore(c => c.LastName);
                entity.Ignore(c => c.EmailAddress);
            });

            modelBuilder.Entity<SalesOrder>(entity =>
            {
                entity.ToTable("SalesOrderHeader", "Sales");
                entity.HasKey(e => e.SalesOrderId);
                entity.Property(e => e.SalesOrderId).HasColumnName("SalesOrderID");
            });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person", "Person");
                entity.HasKey(e => e.BusinessEntityId);
                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.ToTable("EmailAddress", "Person");
                entity.HasKey(e => e.BusinessEntityId);
                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");
            });
            modelBuilder.Entity<SalesOrderDetail>(entity =>
            {
                entity.ToTable("SalesOrderDetail", "Sales");
                //looks like we need combined key of SalesId & OrderId
                entity.HasKey(e => new { e.SalesOrderId, e.SalesOrderDetailId });

                entity.Property(e => e.SalesOrderId).HasColumnName("SalesOrderID");
                entity.Property(e => e.SalesOrderDetailId).HasColumnName("SalesOrderDetailID");
                entity.Property(e => e.ProductId).HasColumnName("ProductID");
                entity.Property(e => e.OrderQty).HasColumnName("OrderQty");
                entity.Property(e => e.LineTotal)
                      .HasColumnName("LineTotal")
                      .HasColumnType("money");
            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product", "Production");
                entity.HasKey(e => e.ProductId);
                entity.Property(e => e.ProductId).HasColumnName("ProductID");
                entity.Property(e => e.Name).HasColumnName("Name");
            });
        }
    }
}
