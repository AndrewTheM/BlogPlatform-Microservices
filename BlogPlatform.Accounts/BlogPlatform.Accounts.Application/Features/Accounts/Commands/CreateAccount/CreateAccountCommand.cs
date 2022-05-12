using BlogPlatform.Accounts.Application.Common.DTO;
using MediatR;

namespace BlogPlatform.Accounts.Application.Features.Accounts.Commands.CreateAccount;

public class CreateAccountCommand : IRequest<AccountDto>
{
    public Guid UserId { get; set; }
}
