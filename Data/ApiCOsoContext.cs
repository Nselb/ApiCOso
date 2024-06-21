using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiCOso;

namespace ApiCOso.Data
{
    public class ApiCOsoContext : DbContext
    {
        public ApiCOsoContext (DbContextOptions<ApiCOsoContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>()
                .HasOne(b => b.Order)
                .WithMany(a => a.OrderDetails)
                .HasForeignKey(b => b.OrderId);
        }
    }
}
