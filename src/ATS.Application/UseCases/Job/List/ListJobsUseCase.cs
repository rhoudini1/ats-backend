using ATS.Application.Mapping;
using ATS.Application.Validators.Common;
using ATS.Contracts.Requests;
using ATS.Contracts.Responses;
using ATS.Domain.Exceptions;
using ATS.Domain.Interfaces.Repositories;

namespace ATS.Application.UseCases.Job.List;

public class ListJobsUseCase : IListJobsUseCase
{
    private readonly IJobRepository _jobRepository;

    public ListJobsUseCase(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<PagedResponse<JobResponse>> Execute(ListRequest request, CancellationToken token)
    {
        Validate(request);

        var jobs = await _jobRepository.GetPagedAsync(request.Page, request.Size, token);
        int totalCount = await _jobRepository.CountTotalAsync(token);

        return new PagedResponse<JobResponse>
        {
            Items = jobs.Select(job => job.MapToResponse()),
            PageNumber = request.Page,
            PageSize = request.Size,
            TotalCount = totalCount,
        };
    }

    private void Validate(ListRequest request)
    {
        var validator = new ListRequestValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}