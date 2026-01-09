using ATS.Application.Mapping;
using ATS.Contracts.Requests;
using ATS.Contracts.Responses;
using ATS.Domain.Entities;
using ATS.Domain.Exceptions;
using ATS.Domain.Exceptions.Base;
using ATS.Domain.Interfaces.Repositories;
using FluentValidation.Results;

namespace ATS.Application.UseCases.JobApplication.Register;

public class RegisterJobApplicationUseCase : IRegisterJobApplicationUseCase
{
    private readonly IJobApplicationRepository _applicationRepository;
    private readonly ICandidateRepository _candidateRepository;
    private readonly IJobRepository _jobRepository;

    public RegisterJobApplicationUseCase(
        IJobApplicationRepository applicationRepository,
        ICandidateRepository candidateRepository,
        IJobRepository jobRepository)
    {
        _applicationRepository = applicationRepository;
        _candidateRepository = candidateRepository;
        _jobRepository = jobRepository;
    }

    public async Task<JobApplicationResponse> Execute(
        RegisterJobApplicationRequest request,
        CancellationToken token)
    {
        var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId, token);
        var job = await _jobRepository.GetByIdAsync(request.JobId, token);

        await Validate(request, candidate, job, token);

        var application = request.MapToJobApplication();
        application.CandidateName = candidate!.FullName;
        application.JobTitle = job!.Title;

        var result = await _applicationRepository.CreateAsync(application, token);

        return result.MapToResponse();
    }

    private async Task Validate(
        RegisterJobApplicationRequest request,
        Domain.Entities.Candidate? candidate,
        Domain.Entities.Job? job,
        CancellationToken token)
    {
        var errors = new List<ValidationFailure>();

        if (candidate is null)
            errors.Add(new ValidationFailure(nameof(request.CandidateId), ErrorMessages.Errors.CandidateNotFound));

        if (job is null)
            errors.Add(new ValidationFailure(nameof(request.JobId), ErrorMessages.Errors.JobNotFound));

        if (candidate is not null && job is not null)
        {
            var alreadyApplied = await _applicationRepository.AlreadyAppliedAsync(request.CandidateId, request.JobId, token);
            if (alreadyApplied)
                errors.Add(new ValidationFailure(string.Empty, ErrorMessages.Validation.AlreadyAppliedToJob));
        }

        if (errors.Count != 0)
            throw new ErrorOnValidationException(errors.Select(e => e.ErrorMessage).ToList());
    }
}