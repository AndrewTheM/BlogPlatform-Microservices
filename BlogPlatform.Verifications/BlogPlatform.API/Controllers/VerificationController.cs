using BlogPlatform.DataAccess.Context.Contracts;
using BlogPlatform.DataAccess.Entities;
using BlogPlatform.DataAccess.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.API.Controllers
{
    [Route("api/verification")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class VerificationController : ControllerBase
    {
        private readonly IBloggingUnitOfWork _unitOfWork;

        public VerificationController(IBloggingUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // TODO: implement properly with business logic

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IQueryable<AuthorVerification>> GetVerifications()
        {
            return await _unitOfWork.AuthorVerifications.GetAllAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthorVerification>> GetVerificationById([FromRoute] int id)
        {
            try
            {
                return await _unitOfWork.AuthorVerifications.GetByIdAsync(id);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<AuthorVerification>> CreateVerification([FromBody] AuthorVerification verificationDto)
        {
            await _unitOfWork.AuthorVerifications.CreateAsync(verificationDto);
            await _unitOfWork.CommitAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateVerification([FromBody] AuthorVerification verificationDto,
                                                           [FromRoute] int id)
        {
            try
            {
                var verification = await _unitOfWork.AuthorVerifications.GetByIdAsync(id);

                verification.PromptText = verificationDto.PromptText;
                verification.Response = verificationDto.Response;
                verification.VerificationStatusId = verificationDto.VerificationStatusId;

                await _unitOfWork.CommitAsync();
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteVerification([FromRoute] int id)
        {
            try
            {
                await _unitOfWork.AuthorVerifications.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
