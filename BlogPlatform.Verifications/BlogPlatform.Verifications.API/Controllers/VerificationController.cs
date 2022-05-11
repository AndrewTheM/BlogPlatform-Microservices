using BlogPlatform.Verifications.DataAccess.Context.Contracts;
using BlogPlatform.Verifications.DataAccess.Entities;
using BlogPlatform.Verifications.DataAccess.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Verifications.API.Controllers;

[Route("api/verification")]
[ApiController]
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
    public async Task<IQueryable<AuthorVerification>> GetVerifications()
    {
        return await _unitOfWork.AuthorVerifications.GetAllAsync();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AuthorVerification>> GetVerificationById([FromRoute] Guid id)
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
    public async Task<ActionResult<AuthorVerification>> CreateVerification(
        [FromBody] AuthorVerification verificationDto)
    {
        await _unitOfWork.AuthorVerifications.CreateAsync(verificationDto);
        await _unitOfWork.CommitAsync();
        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateVerification(
        [FromRoute] Guid id, [FromBody] AuthorVerification verificationDto)
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteVerification([FromRoute] Guid id)
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
