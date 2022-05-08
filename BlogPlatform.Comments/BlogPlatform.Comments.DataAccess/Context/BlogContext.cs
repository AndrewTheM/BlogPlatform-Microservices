using BlogPlatform.Comments.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlogPlatform.Comments.DataAccess.Context
{
    public class BlogContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

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
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            var entities = ChangeTracker.Entries()
                                        .Where(entry => entry.State == EntityState.Modified)
                                        .Select(entry => entry.Entity)
                                        .OfType<EntityBase<int>>();

            foreach (var entity in entities)
                entity.UpdatedOn = DateTime.Now;

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
