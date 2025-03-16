using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Models;

namespace UserManagement.Infrastructure;

public class ApplicationContext (DbContextOptions<ApplicationContext> options)
    : IdentityDbContext<User, IdentityRole, string>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

        modelBuilder.Entity<User>();
    }
    
    public new DbSet<User> Users { get; set; }
}