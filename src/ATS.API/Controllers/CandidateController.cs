using ATS.Application.UseCases.Candidate.GetById;
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
        [FromBody] RegisterCandidateRequest request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
}
