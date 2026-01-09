using ATS.Application.UseCases.Candidate.Delete;
using ATS.Application.UseCases.Candidate.GetById;
using ATS.Application.UseCases.Candidate.List;
using ATS.Application.UseCases.Candidate.ListApplications;
using ATS.Application.UseCases.Candidate.Register;
using ATS.Application.UseCases.Candidate.Update;
using ATS.Contracts.Requests;
using ATS.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace ATS.API.Controllers;

[ApiController]
public class CandidateController : ControllerBase
{
    private readonly IOutputCacheStore _outputCacheStore;

    public CandidateController(IOutputCacheStore outputCacheStore)
    {
        _outputCacheStore = outputCacheStore;
    }

    [HttpPost(ApiEndpoints.Candidate.Create)]
    [ProducesResponseType(typeof(CandidateResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterCandidateUseCase useCase,
        [FromBody] RegisterCandidateRequest request,
        CancellationToken token)
    {
        var result = await useCase.Execute(request, token);

        await _outputCacheStore.EvictByTagAsync("candidates", token);

        return Created(string.Empty, result);
    }

    [HttpGet(ApiEndpoints.Candidate.GetById)]
    [OutputCache(PolicyName = "CandidatesCache")]
    [ProducesResponseType(typeof(CandidateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCandidate(
        [FromServices] IGetCandidateByIdUseCase useCase,
        [FromRoute] Guid id,
        CancellationToken token)
    {
        var result = await useCase.Execute(id, token);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet(ApiEndpoints.Candidate.List)]
    [OutputCache(PolicyName = "CandidatesCache")]
    [ProducesResponseType(typeof(CandidateResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListCandidates(
        [FromServices] IListCandidatesUseCase useCase,
        [FromQuery] ListRequest request,
        CancellationToken token)
    {
        var result = await useCase.Execute(request, token);

        return Ok(result);
    }

    [HttpGet(ApiEndpoints.Candidate.Applications)]
    [OutputCache(PolicyName = "AppsByCandidatePolicy")]
    [ProducesResponseType(typeof(CandidateResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetApplications(
        [FromServices] IListCandidateApplicationsUseCase useCase,
        [FromRoute] Guid id,
        [FromQuery] ListRequest request,
        CancellationToken token)
    {
        var result = await useCase.Execute(id, request, token);

        return Ok(result);
    }

    [HttpPut(ApiEndpoints.Candidate.Update)]
    [ProducesResponseType(typeof(CandidateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCandidate(
        [FromServices] IUpdateCandidateUseCase useCase,
        [FromRoute] Guid id,
        [FromBody] UpdateCandidateRequest request,
        CancellationToken token)
    {
        var result = await useCase.Execute(id, request, token);

        await _outputCacheStore.EvictByTagAsync("candidates", token);

        return Ok(result);
    }

    [HttpDelete(ApiEndpoints.Candidate.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCandidate(
        [FromServices] IDeleteCandidateByIdUseCase useCase,
        [FromRoute] Guid id,
        CancellationToken token)
    {
        bool deleted = await useCase.Execute(id, token);

        if (!deleted)
            return NotFound();

        await _outputCacheStore.EvictByTagAsync("candidates", token);

        return NoContent();
    }
}
