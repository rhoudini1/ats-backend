using ATS.Application.UseCases.Job.Register;
using ATS.Contracts.Requests;
using ATS.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace ATS.API.Controllers;

[ApiController]
public class JobController : ControllerBase
{
    private readonly IOutputCacheStore _outputCacheStore;

    public JobController(IOutputCacheStore outputCacheStore)
    {
        _outputCacheStore = outputCacheStore;
    }

    [HttpPost(ApiEndpoints.Job.Create)]
    [ProducesResponseType(typeof(CandidateResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterJobUseCase useCase,
        [FromBody] RegisterJobRequest request,
        CancellationToken token)
    {
        var result = await useCase.Execute(request, token);

        await _outputCacheStore.EvictByTagAsync("job", token);

        return Created(string.Empty, result);
    }
}
