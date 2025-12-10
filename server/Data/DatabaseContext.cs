using KePass.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace KePass.Server.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Audit> Audits { get; set; }
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
        
        base.OnModelCreating(modelBuilder);
    }
}