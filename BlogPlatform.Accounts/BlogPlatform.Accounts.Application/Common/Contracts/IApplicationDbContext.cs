﻿using BlogPlatform.Accounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Accounts.Application.Common.Contracts;

public interface IApplicationDbContext
{
    DbSet<AuthorApplication> AuthorApplications { get; set; }

    DbSet<ApplicationFeedback> ApplicationFeedbacks { get; set; }

    DbSet<Account> Accounts { get; set; }

    Task<int> SaveChangesAsync(CancellationToken token);
}
