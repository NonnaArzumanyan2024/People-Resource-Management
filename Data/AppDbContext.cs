using Microsoft.EntityFrameworkCore;
using People_Specification.Api.Models;

namespace People_Specification.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Unit> Units { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Unit>()
            .HasOne(unit => unit.ParentUnit)
            .WithMany(unit => unit.ChildUnits)
            .HasForeignKey(unit => unit.ParentUnitId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Employee>()
            .HasOne(employee => employee.Unit)
            .WithMany(unit => unit.Employees)
            .HasForeignKey(employee => employee.UnitId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
