using Accounts.Application.Common.DTO;
using Accounts.Application.Features.Accounts.Commands.CreateAccount;
using Accounts.Application.Features.Accounts.Commands.EditAccount;
using Accounts.Application.Features.Accounts.Commands.DeleteAccount;
using Accounts.Application.Features.Accounts.Queries.GetAccount;
using Accounts.Application.Features.Accounts.Queries.GetAccountOfUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Accounts.API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly ISender _mediator;

        public AccountController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountDto>> GetAccount([FromRoute] Guid id)
        {
            var query = new GetAccountQuery { Id = id };
            var account = await _mediator.Send(query);
            return Ok(account);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountDto>> GetAccountOfUser([FromRoute] Guid userId)
        {
            var query = new GetAccountOfUserQuery { UserId = userId };
            var account = await _mediator.Send(query);
            return Ok(account);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AccountDto>> CreateAccount(
            [FromBody] CreateAccountCommand command)
        {
            var account = await _mediator.Send(command);
            return Ok(account);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> EditAccount(
            [FromBody] EditAccountCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAccount([FromRoute] Guid id)
        {
            var command = new DeleteAccountCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
