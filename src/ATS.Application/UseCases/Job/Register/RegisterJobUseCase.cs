using ATS.Application.Mapping;
using ATS.Application.Validators.Job;
using ATS.Contracts.Requests;
using ATS.Contracts.Responses;
using ATS.Domain.Exceptions;
using ATS.Domain.Interfaces.Repositories;

namespace ATS.Application.UseCases.Job.Register;

public class RegisterJobUseCase : IRegisterJobUseCase
{
    private readonly IJobRepository _jobRepository;

    public RegisterJobUseCase(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<JobResponse> Execute(RegisterJobRequest request, CancellationToken token)
    {
        Validate(request);

        var job = request.MapToJob();

        var result = await _jobRepository.CreateAsync(job, token);

        return result.MapToResponse();
    }

    private void Validate(RegisterJobRequest request)
    {
        var validator = new RegisterJobValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
