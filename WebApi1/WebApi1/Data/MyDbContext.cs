using Microsoft.EntityFrameworkCore;
using WebApi1.Models;

namespace WebApi1.Data
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(f => f.Warehouse)
                .WithMany()
                .HasForeignKey(f => f.WarehouseId)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SalaryLog>()
            .HasOne<Employee>(s => s.Employee)
            .WithMany()
            .HasForeignKey(s => s.EmployeeId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
