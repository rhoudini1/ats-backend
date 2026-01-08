using ATS.Application.UseCases.Candidate.Delete;
using ATS.Application.UseCases.Candidate.GetById;
using ATS.Application.UseCases.Candidate.List;
using ATS.Application.UseCases.Candidate.Register;
using ATS.Application.UseCases.Candidate.Update;
using ATS.Contracts.Requests;
using ATS.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ATS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CandidateController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CandidateResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterCandidateUseCase useCase,
        [FromBody] RegisterCandidateRequest request,
        CancellationToken token)
    {
        var result = await useCase.Execute(request, token);

        return Created(string.Empty, result);
    }

    [HttpGet("{id:guid}")]
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

    [HttpGet]
    [ProducesResponseType(typeof(CandidateResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListCandidates(
        [FromServices] IListCandidatesUseCase useCase,
        [FromQuery] ListRequest request,
        CancellationToken token)
    {
        var result = await useCase.Execute(request, token);

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CandidateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> UpdateCandidate(
        [FromServices] IUpdateCandidateUseCase useCase,
        [FromRoute] Guid id,
        [FromBody] UpdateCandidateRequest request,
        CancellationToken token)
    {
        var result = await useCase.Execute(id, request, token);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
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

        return NoContent();
    }
}
