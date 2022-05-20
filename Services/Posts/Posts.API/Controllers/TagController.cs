﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Posts.BusinessLogic.DTO.Requests;
using Posts.BusinessLogic.DTO.Responses;
using Posts.DataAccess.Context.Contracts;
using Posts.DataAccess.Entities;

namespace Posts.API.Controllers;

[Route("api/posts/tags")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly IBloggingUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TagController(IBloggingUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IEnumerable<string>> GetTags()
    {
        var tags = await _unitOfWork.Tags.GetRelevantTagsAsync();
        return tags.Select(t => t.TagName);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TagResponse>> GetTagById([FromRoute] Guid id)
    {
        var tag = await _unitOfWork.Tags.GetByIdAsync(id);
        return _mapper.Map<TagResponse>(tag);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> CreateTag([FromBody] TagRequest tagDto)
    {
        var tag = _mapper.Map<Tag>(tagDto);
        await _unitOfWork.Tags.CreateAsync(tag);
        await _unitOfWork.CommitAsync();
        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateTag(
        [FromRoute] Guid id, [FromBody] TagRequest tagDto)
    {
        var tag = await _unitOfWork.Tags.GetByIdAsync(id);
        _mapper.Map(tagDto, tag);
        await _unitOfWork.CommitAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteTag([FromRoute] Guid id)
    {
        await _unitOfWork.Tags.DeleteAsync(id);
        await _unitOfWork.CommitAsync();
        return NoContent();
    }
}