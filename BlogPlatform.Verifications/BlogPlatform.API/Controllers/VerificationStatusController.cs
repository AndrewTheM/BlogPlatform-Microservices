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
    [Route("api/verification/statuses")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class VerificationStatusController : ControllerBase
    {
        private readonly IBloggingUnitOfWork _unitOfWork;

        public VerificationStatusController(IBloggingUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // TODO: implement properly with business logic

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IQueryable<VerificationStatus>> GetStatuses()
        {
            return await _unitOfWork.VerificationStatuses.GetAllAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<VerificationStatus>> GetStatusById([FromRoute] int id)
        {
            try
            {
                return await _unitOfWork.VerificationStatuses.GetByIdAsync(id);
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
        public async Task<ActionResult<VerificationStatus>> CreateStatus([FromBody] VerificationStatus statusDto)
        {
            await _unitOfWork.VerificationStatuses.CreateAsync(statusDto);
            await _unitOfWork.CommitAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateStatus([FromBody] VerificationStatus statusDto,
                                                     [FromRoute] int id)
        {
            try
            {
                var status = await _unitOfWork.VerificationStatuses.GetByIdAsync(id);
                status.StatusName = statusDto.StatusName;
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
        public async Task<ActionResult> DeleteStatus([FromRoute] int id)
        {
            try
            {
                await _unitOfWork.VerificationStatuses.DeleteAsync(id);
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
