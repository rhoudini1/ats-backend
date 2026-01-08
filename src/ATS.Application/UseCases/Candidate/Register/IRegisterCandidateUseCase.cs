using ATS.Contracts.Requests;
using ATS.Contracts.Responses;

namespace ATS.Application.UseCases.Candidate.Register;

public interface IRegisterCandidateUseCase
{
    Task<CandidateResponse> Execute(RegisterCandidateRequest request, CancellationToken cancellationToken);
}
