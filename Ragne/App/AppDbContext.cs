using Microsoft.EntityFrameworkCore;
using Ragne.Features.en;
using Ragne.Features.to;
using Ragne.Features.tre;
using Ragne.Features.fire;
using Ragne.Features.fem;
using Ragne.Features.seks;

namespace Ragne.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<enModel> en { get; set; }

    public DbSet<toModel> to { get; set; }

    public DbSet<treModel> tre { get; set; }

    public DbSet<fireModel> fire { get; set; }

    public DbSet<femModel> fem { get; set; }

    public DbSet<seksModel> seks { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<enModel>().HasKey(e => e.Id);

        modelBuilder.Entity<toModel>().HasKey(e => e.Id);

        modelBuilder.Entity<treModel>().HasKey(e => e.Id);

        modelBuilder.Entity<fireModel>().HasKey(e => e.Id);

        modelBuilder.Entity<femModel>().HasKey(e => e.Id);

        modelBuilder.Entity<seksModel>().HasKey(e => e.Id);
    }
}
