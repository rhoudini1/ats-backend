using ATS.Contracts.Requests;
using ATS.Contracts.Responses;

namespace ATS.Application.UseCases.Candidate.Update;

public interface IUpdateCandidateUseCase
{
    Task<CandidateResponse> Execute(Guid id, UpdateCandidateRequest request, CancellationToken cancellationToken);
}
