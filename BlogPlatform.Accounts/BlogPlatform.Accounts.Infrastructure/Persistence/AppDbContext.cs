using BlogPlatform.Accounts.Application.Common.Contracts;
using BlogPlatform.Accounts.Domain.Abstract;
using BlogPlatform.Accounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BlogPlatform.Accounts.Infrastructure.Persistence;

public class AppDbContext : DbContext, IApplicationDbContext
{
    public DbSet<AuthorApplication> AuthorApplications { get; set; }

    public DbSet<ApplicationFeedback> ApplicationFeedbacks { get; set; }

    public DbSet<Account> Accounts { get; set; }

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions contextOptions)
        : base(contextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.DetectChanges();

        var entities = ChangeTracker.Entries()
            .Where(entry => entry.State == EntityState.Modified)
            .Select(entry => entry.Entity)
            .OfType<EntityBase>();

        foreach (var entity in entities)
        {
            entity.UpdatedOn = DateTime.Now;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
