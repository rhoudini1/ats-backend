using ATS.Application.Mapping;
using ATS.Application.Validators.Common;
using ATS.Contracts.Requests;
using ATS.Contracts.Responses;
using ATS.Domain.Exceptions;
using ATS.Domain.Exceptions.Base;
using ATS.Domain.Interfaces.Repositories;

namespace ATS.Application.UseCases.Candidate.ListApplications;

public class ListCandidateApplicationsUseCase : IListCandidateApplicationsUseCase
{
    private readonly IJobApplicationRepository _applicationRepository;
    private readonly ICandidateRepository _candidateRepository;

    public ListCandidateApplicationsUseCase(
        IJobApplicationRepository applicationRepository,
        ICandidateRepository candidateRepository)
    {
        _applicationRepository = applicationRepository;
        _candidateRepository = candidateRepository;
    }

    public async Task<PagedResponse<JobApplicationResponse>> Execute(
        Guid candidateId,
        ListRequest request,
        CancellationToken token)
    {
        var candidate = await _candidateRepository.GetByIdAsync(candidateId, token);

        Validate(request, candidate);

        var (items, totalCount) = await _applicationRepository
            .GetPagedByCandidateIdAsync(candidateId, request.Page, request.Size, token);

        return new PagedResponse<JobApplicationResponse>
        {
            Items = items.Select(application => application.MapToResponse()),
            PageNumber = request.Page,
            PageSize = request.Size,
            TotalCount = totalCount
        };
    }

    private void Validate(ListRequest request, Domain.Entities.Candidate? candidate)
    {
        if (candidate is null)
        {
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.Errors.CandidateNotFound });
        }

        var validator = new ListRequestValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
