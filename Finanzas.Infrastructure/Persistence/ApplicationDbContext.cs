using System.Collections.Generic;
using System.Reflection.Emit;
using Finanzas.Domain.Aggregates;
using Finanzas.Domain.Entities;
using Finanzas.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Finanzas.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Movement> Movements => Set<Movement>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.UserId)
                .IsRequired();

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

            entity.HasIndex(e => new { e.UserId, e.Name }).IsUnique();

            entity.HasOne<IdentityUser<Guid>>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Movement>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();

            entity.OwnsOne(a => a.Value, b =>
            {
                b.Property(p => p.Amount).HasColumnName("Value");
                b.Property(p => p.Currency).HasColumnName("ValueCurrency");
            });
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.UserId)
               .IsRequired();

            entity.Property(a => a.Name).IsRequired().HasMaxLength(100);

            entity.OwnsOne(a => a.Balance, b =>
            {
                b.Property(p => p.Amount).HasColumnName("BalanceAmount");
                b.Property(p => p.Currency).HasColumnName("BalanceCurrency");
            });

            entity.OwnsOne(a => a.InitialBalance, b =>
            {
                b.Property(p => p.Amount).HasColumnName("InitialAmount");
                b.Property(p => p.Currency).HasColumnName("InitialCurrency");
            });

            entity.HasIndex(e => new { e.UserId, e.Name }).IsUnique();

            entity.HasOne<IdentityUser<Guid>>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(a => a.Movements)
                   .WithOne()
                   .HasForeignKey("AccountId")
                   .OnDelete(DeleteBehavior.Cascade);
        });
    }
}