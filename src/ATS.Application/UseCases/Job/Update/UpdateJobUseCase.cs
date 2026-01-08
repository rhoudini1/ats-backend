using ATS.Application.Mapping;
using ATS.Application.Validators.Job;
using ATS.Contracts.Requests;
using ATS.Contracts.Responses;
using ATS.Domain.Enums;
using ATS.Domain.Exceptions;
using ATS.Domain.Exceptions.Base;
using ATS.Domain.Interfaces.Repositories;
using FluentValidation.Results;

namespace ATS.Application.UseCases.Job.Update;

public class UpdateJobUseCase : IUpdateJobUseCase
{
    private readonly IJobRepository _jobRepository;

    public UpdateJobUseCase(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<JobResponse> Execute(Guid id, UpdateJobRequest request, CancellationToken token)
    {
        var job = await Validate(id, request, token);

        job.Title = request.Title;
        job.Description = request.Description;
        job.Status = Enum.Parse<EJobStatus>(request.Status, ignoreCase: true);
        job.UpdatedAt = DateTime.UtcNow;

        var result = await _jobRepository.UpdateAsync(job, token);

        return result.MapToResponse();
    }

    private async Task<Domain.Entities.Job> Validate(Guid id, UpdateJobRequest request, CancellationToken token)
    {
        var validator = new UpdateJobValidator();
        var result = validator.Validate(request);

        var job = await _jobRepository.GetByIdAsync(id, token);

        if (job is null)
            result.Errors.Add(new ValidationFailure(string.Empty, ErrorMessages.Errors.JobNotFound));

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }

        return job!;
    }
}