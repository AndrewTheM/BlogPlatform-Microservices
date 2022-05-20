using Accounts.Application.Common.Contracts;
using Accounts.Application.Common.Exceptions;
using MediatR;

namespace Accounts.Application.Features.Accounts.Commands.DeleteAccount;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteAccountCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Accounts
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (account is null)
        {
            throw new EntityNotFoundException();
        }

        _dbContext.Accounts.Remove(account);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
