using KePass.Server.Extensions;
using KePass.Server.Models;
using KePass.Server.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace KePass.Server.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Audit> Audits { get; set; }
    public DbSet<Blob> Blobs { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Vault> Vaults { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=local.sqlite");

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(x => x.HasKey(e => e.Id));
        modelBuilder.Entity<Attachment>(x => x.HasKey(e => e.Id));
        modelBuilder.Entity<Audit>(x => x.HasKey(e => e.Id));
        modelBuilder.Entity<Subscription>(x => x.HasKey(e => e.Id));
        modelBuilder.Entity<Vault>(x => x.HasKey(e => e.Id));
        
        modelBuilder.Entity<Account>().HasIndex(e => e.Username).IsUnique();
        modelBuilder.Entity<Account>().HasIndex(e => e.Email).IsUnique();
        modelBuilder.Entity<Attachment>().HasIndex(e => e.Name).IsUnique();
        modelBuilder.Entity<Blob>().HasIndex(e => e.Name).IsUnique();
        modelBuilder.Entity<Subscription>().HasIndex(e => e.PaymentId).IsUnique();
        
        modelBuilder.Entity<Account>().Property(x => x.Email).HasConversion(Email.GetValueConverter());
        modelBuilder.Entity<Account>().Property(x => x.Password).HasConversion(Password.GetValueConverter());
        modelBuilder.Entity<Vault>().Property(x => x.Key).HasConversion(Key.GetValueConverter());
        modelBuilder.Entity<Vault>().Property(x => x.Version).HasConversion(VersionExtension.GetValueConverter());
        
        base.OnModelCreating(modelBuilder);
    }
}