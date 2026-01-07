using ATS.Contracts.Requests;
using ATS.Contracts.Responses;

namespace ATS.Application.UseCases.Candidate.Register;

public interface IRegisterCandidateUseCase
{
    Task<RegisterCandidateResponse> Execute(RegisterCandidateRequest request);
}
