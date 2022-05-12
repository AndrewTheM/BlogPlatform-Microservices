using BlogPlatform.Accounts.Application.Common.DTO;
using BlogPlatform.Accounts.Application.Features.Accounts.Commands.CreateAccount;
using BlogPlatform.Accounts.Application.Features.Accounts.Commands.DeleteAccount;
using BlogPlatform.Accounts.Application.Features.Accounts.Commands.EditAccount;
using BlogPlatform.Accounts.Application.Features.Accounts.Queries.GetAccount;
using BlogPlatform.Accounts.Application.Features.Accounts.Queries.GetAccountOfUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Accounts.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ISender _mediator;

        public AccountController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountDto>> GetAccount([FromRoute] Guid id)
        {
            var query = new GetAccountQuery { Id = id };
            return await _mediator.Send(query);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountDto>> GetAccountOfUser([FromRoute] Guid userId)
        {
            var query = new GetAccountOfUserQuery { UserId = userId };
            return await _mediator.Send(query);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AccountDto>> CreateAccount(
            [FromBody] CreateAccountCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> EditAccount(
            [FromBody] EditAccountCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAccount([FromRoute] Guid id)
        {
            var command = new DeleteAccountCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
