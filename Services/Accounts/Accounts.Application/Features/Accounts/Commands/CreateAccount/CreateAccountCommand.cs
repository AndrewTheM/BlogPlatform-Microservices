using Accounts.Application.Common.DTO;
using MediatR;

namespace Accounts.Application.Features.Accounts.Commands.CreateAccount;

public class CreateAccountCommand : IRequest<AccountDto>
{
    public Guid UserId { get; set; }
}
