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
}