using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TinyCrm.Core.Model;

namespace TinyCrm.Core.Data
{
    public class TinyCrmDbContext : DbContext
    {
        private readonly string connectionString_;

        /// <summary>
        /// Parameterless constructor required for Migrations
        /// to run
        /// </summary>
        public TinyCrmDbContext() : base()
        {
            connectionString_ = "Server=localhost;Database=tinycrm;User Id=sa;Password=QWE123!@#";
        }

        public TinyCrmDbContext(string connString)
        {
            connectionString_ = connString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Product>()
                .ToTable("Product");

            modelBuilder
                .Entity<Customer>()
                .ToTable("Customer");

            modelBuilder
                .Entity<Order>()
                .ToTable("Order");

            modelBuilder
                .Entity<ContactPerson>()
                .ToTable("ContactPerson");

            //modelBuilder
            //    .Entity<Order>()
            //    .ToTable("Order");

            //modelBuilder
            //    .Entity<OrderProduct>()
            //    .HasKey(op => new { op.OrderId, op.ProductId });

            //modelBuilder
            //    .Entity<OrderProduct>()
            //    .HasOne(op => op.Order)
            //    .WithMany(o => o.Products)
            //    .HasForeignKey(o => o.OrderId);

            //modelBuilder
            //    .Entity<OrderProduct>()
            //    .HasOne(op => op.Product)
            //    .WithMany();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString_);
        }
    }
}
