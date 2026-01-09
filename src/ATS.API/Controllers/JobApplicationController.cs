using ATS.Application.UseCases.JobApplication.Register;
using ATS.Contracts.Requests;
using ATS.Contracts.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace ATS.API.Controllers;

[ApiController]
public class JobApplicationController : ControllerBase
{
    private readonly IOutputCacheStore _outputCacheStore;

    public JobApplicationController(IOutputCacheStore outputCacheStore)
    {
        _outputCacheStore = outputCacheStore;
    }

    [HttpPost(ApiEndpoints.JobApplication.Register)]
    [ProducesResponseType(typeof(JobApplicationResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterJobApplicationUseCase useCase,
        [FromBody] RegisterJobApplicationRequest request,
        CancellationToken token)
    {
        var result = await useCase.Execute(request, token);

        await _outputCacheStore.EvictByTagAsync($"candidate-apps-{request.CandidateId}", token);
        await _outputCacheStore.EvictByTagAsync($"job-apps-{request.JobId}", token);

        return Created(string.Empty, result);
    }
}
