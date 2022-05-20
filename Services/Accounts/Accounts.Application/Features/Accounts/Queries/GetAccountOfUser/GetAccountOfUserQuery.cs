using Accounts.Application.Common.DTO;
using MediatR;

namespace Accounts.Application.Features.Accounts.Queries.GetAccountOfUser;

public class GetAccountOfUserQuery : IRequest<AccountDto>
{
    public Guid UserId { get; set; }
}
