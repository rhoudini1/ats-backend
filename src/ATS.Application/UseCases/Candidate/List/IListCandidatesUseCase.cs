using ATS.Contracts.Requests;
using ATS.Contracts.Responses;

namespace ATS.Application.UseCases.Candidate.List;

public interface IListCandidatesUseCase
{
    Task<PagedResponse<CandidateResponse>> Execute(ListRequest request, CancellationToken cancellationToken);
}
