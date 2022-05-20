using Accounts.Application.Common.Contracts;
using Accounts.Application.Common.DTO;
using Accounts.Application.Common.Exceptions;
using AutoMapper;
using MediatR;

namespace Accounts.Application.Features.Accounts.Queries.GetAccount;

public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, AccountDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAccountQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<AccountDto> Handle(
        GetAccountQuery request, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Accounts
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (account is null)
        {
            throw new EntityNotFoundException();
        }

        return _mapper.Map<AccountDto>(account);
    }
}
