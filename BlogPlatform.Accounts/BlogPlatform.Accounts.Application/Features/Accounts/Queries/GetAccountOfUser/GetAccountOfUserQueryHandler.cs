using AutoMapper;
using BlogPlatform.Accounts.Application.Common.Contracts;
using BlogPlatform.Accounts.Application.Common.DTO;
using BlogPlatform.Accounts.Application.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Accounts.Application.Features.Accounts.Queries.GetAccountOfUser;

public class GetAccountOfUserQueryHandler : IRequestHandler<GetAccountOfUserQuery, AccountDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAccountOfUserQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<AccountDto> Handle(
        GetAccountOfUserQuery request, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Accounts
            .SingleOrDefaultAsync(ac => ac.UserId == request.UserId, cancellationToken);

        if (account is null)
        {
            throw new EntityNotFoundException();
        }

        return _mapper.Map<AccountDto>(account);
    }
}
