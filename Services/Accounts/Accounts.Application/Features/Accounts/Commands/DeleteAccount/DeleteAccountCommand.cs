using MediatR;

namespace Accounts.Application.Features.Accounts.Commands.DeleteAccount;

public class DeleteAccountCommand : IRequest
{
    public Guid Id { get; set; }
}
