using System.Collections.Generic;
using System.Reflection.Emit;
using Finanzas.Domain.Aggregates;
using Finanzas.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;


namespace Finanzas.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .HasConversion(
                    v => v.Value,
                    v => new CategoryName(v))
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Color)
                .HasConversion(
                    v => v.Value,
                    v => new CategoryColor(v))
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(e => e.Icon)
                .IsRequired();

            entity.HasIndex(e => e.Name).IsUnique();
        });
    }
}