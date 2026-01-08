using ATS.Application.Mapping;
using ATS.Application.Validators.Common;
using ATS.Contracts.Requests;
using ATS.Contracts.Responses;
using ATS.Domain.Exceptions;
using ATS.Domain.Interfaces.Repositories;

namespace ATS.Application.UseCases.Candidate.List;

public class ListCandidatesUseCase : IListCandidatesUseCase
{
    private readonly ICandidateRepository _candidateRepository;

    public ListCandidatesUseCase(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<PagedResponse<CandidateResponse>> Execute(ListRequest request)
    {
        Validate(request);

        var candidates = await _candidateRepository.GetPagedAsync(request.Page, request.Size);
        int totalCount = await _candidateRepository.CountTotalAsync();

        return new PagedResponse<CandidateResponse>
        {
            Items = candidates.Select(candidate => candidate.MapToResponse()),
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
