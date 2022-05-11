using BlogPlatform.Verifications.DataAccess.Context.Contracts;
using BlogPlatform.Verifications.DataAccess.Entities;
using BlogPlatform.Verifications.DataAccess.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Verifications.API.Controllers;

[Route("api/verification/statuses")]
[ApiController]
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
    public async Task<IQueryable<VerificationStatus>> GetStatuses()
    {
        return await _unitOfWork.VerificationStatuses.GetAllAsync();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<VerificationStatus>> GetStatusById([FromRoute] Guid id)
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
    public async Task<ActionResult<VerificationStatus>> CreateStatus(
        [FromBody] VerificationStatus statusDto)
    {
        await _unitOfWork.VerificationStatuses.CreateAsync(statusDto);
        await _unitOfWork.CommitAsync();
        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateStatus(
        [FromRoute] Guid id, [FromBody] VerificationStatus statusDto)
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteStatus([FromRoute] Guid id)
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
