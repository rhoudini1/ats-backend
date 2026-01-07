using ATS.Contracts.Responses;

namespace ATS.Application.UseCases.Candidate.GetById;

public interface IGetCandidateByIdUseCase
{
    Task<CandidateResponse> Execute(Guid id);
}
