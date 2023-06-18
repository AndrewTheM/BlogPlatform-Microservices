using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Posts.DataAccess.Entities;

namespace Posts.DataAccess.Context;

public class BlogContext : DbContext
{
    public DbSet<Post> Posts { get; set; }

    public DbSet<PostContent> PostContents { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<Rating> Ratings { get; set; }

    public BlogContext()
    {
        Database.Migrate();
    }

    public BlogContext(DbContextOptions contextOptions) : base(contextOptions)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
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

        return base.SaveChangesAsync(cancellationToken);
    }
}
