using Accounts.Application.Common.Contracts;
using Accounts.Application.Common.DTO;
using Accounts.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Accounts.Application.Features.Accounts.Commands.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateAccountCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<AccountDto> Handle(
        CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var newAccount = new Account { UserId = request.UserId };
        await _dbContext.Accounts.AddAsync(newAccount, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return _mapper.Map<AccountDto>(newAccount);
    }
}
