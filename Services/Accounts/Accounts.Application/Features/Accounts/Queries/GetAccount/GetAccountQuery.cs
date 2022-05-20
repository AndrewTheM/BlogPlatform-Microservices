using Accounts.Application.Common.DTO;
using MediatR;

namespace Accounts.Application.Features.Accounts.Queries.GetAccount;

public class GetAccountQuery : IRequest<AccountDto>
{
    public Guid Id { get; set; }
}
