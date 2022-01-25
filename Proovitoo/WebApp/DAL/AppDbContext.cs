using Microsoft.EntityFrameworkCore;
using WebApp.Domain;

namespace WebApp.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Isik> Isikud { get; set; } = default!;
    public DbSet<Yritus> Yritused { get; set; } = default!;
    public DbSet<Osalemine> Osalemised { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var relationship in modelBuilder.Model
                     .GetEntityTypes()
                     .Where(e => !e.IsOwned())
                     .SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
    }
}