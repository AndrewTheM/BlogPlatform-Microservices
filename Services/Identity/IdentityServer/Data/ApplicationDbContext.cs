using IdentityServer.Data.Entities;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IdentityServer.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    private readonly AdminUserOptions _adminOptions;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<AdminUserOptions> adminOptions)
        : base(options)
    {
        _adminOptions = adminOptions.Value;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        var adminRoleId = Guid.NewGuid();
        var adminUserId = Guid.Parse(_adminOptions.Id);

        builder.Entity<ApplicationRole>(role =>
        {
            role.HasData(
                new() { Id = Guid.NewGuid(), Name = "Reader", NormalizedName = "READER" },
                new() { Id = Guid.NewGuid(), Name = "Author", NormalizedName = "AUTHOR" },
                new() { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" }
            );
        });

        if (_adminOptions is null)
            return;
            
        builder.Entity<ApplicationUser>(user =>
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            user.HasData(
                new ApplicationUser
                {
                    Id = adminUserId,
                    UserName = _adminOptions.Username,
                    NormalizedUserName = _adminOptions.Username.ToUpper(),
                    PasswordHash = hasher.HashPassword(null, _adminOptions.Password),
                    Email = _adminOptions.Email,
                    NormalizedEmail = _adminOptions.Email.ToUpper(),
                    SecurityStamp = Guid.NewGuid().ToString()
                }
            );
        });

        builder.Entity<IdentityUserRole<Guid>>(ur =>
        {
            ur.HasData(
                new IdentityUserRole<Guid>
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                }
            );
        });
    }
}
