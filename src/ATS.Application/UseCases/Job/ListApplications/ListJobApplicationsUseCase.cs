using ATS.Application.Mapping;
using ATS.Application.Validators.Common;
using ATS.Contracts.Requests;
using ATS.Contracts.Responses;
using ATS.Domain.Exceptions;
using ATS.Domain.Exceptions.Base;
using ATS.Domain.Interfaces.Repositories;

namespace ATS.Application.UseCases.Job.ListApplications;

public class ListJobApplicationsUseCase : IListJobApplicationsUseCase
{
    private readonly IJobApplicationRepository _applicationRepository;
    private readonly IJobRepository _jobRepository;

    public ListJobApplicationsUseCase(
        IJobApplicationRepository applicationRepository,
        IJobRepository jobRepository)
    {
        _applicationRepository = applicationRepository;
        _jobRepository = jobRepository;
    }

    public async Task<PagedResponse<JobApplicationResponse>> Execute(
        Guid jobId,
        ListRequest request,
        CancellationToken token)
    {
        var job = await _jobRepository.GetByIdAsync(jobId, token);

        Validate(request, job);

        var (items, totalCount) = await _applicationRepository
            .GetPagedByJobIdAsync(jobId, request.Page, request.Size, token);

        return new PagedResponse<JobApplicationResponse>
        {
            Items = items.Select(app => app.MapToResponse()),
            PageNumber = request.Page,
            PageSize = request.Size,
            TotalCount = totalCount
        };
    }

    private void Validate(ListRequest request, Domain.Entities.Job? job)
    {
        if (job is null)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.Errors.JobNotFound });

        var validator = new ListRequestValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}