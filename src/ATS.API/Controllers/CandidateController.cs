using ATS.Application.UseCases.Candidate.GetById;
using ATS.Application.UseCases.Candidate.List;
using ATS.Application.UseCases.Candidate.Register;
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
}
