using BlogPlatform.Posts.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BlogPlatform.Posts.DataAccess.Context;

public class BlogContext : DbContext
{
    public DbSet<Post> Posts { get; set; }

    public DbSet<PostContent> PostContents { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<Rating> Ratings { get; set; }

    public BlogContext()
    {
    }

    public BlogContext(DbContextOptions contextOptions)
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
            entity.UpdatedOn = DateTime.Now;

        return await base.SaveChangesAsync(cancellationToken);
    }
}
