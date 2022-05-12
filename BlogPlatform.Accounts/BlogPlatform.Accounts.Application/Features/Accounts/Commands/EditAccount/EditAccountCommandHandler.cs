using AutoMapper;
using BlogPlatform.Accounts.Application.Common.Contracts;
using BlogPlatform.Accounts.Application.Common.Exceptions;
using MediatR;

namespace BlogPlatform.Accounts.Application.Features.Accounts.Commands.EditAccount;

public class EditAccountCommandHandler : IRequestHandler<EditAccountCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public EditAccountCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(
        EditAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Accounts
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (account is null)
        {
            throw new EntityNotFoundException();
        }

        _mapper.Map(request, account);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
