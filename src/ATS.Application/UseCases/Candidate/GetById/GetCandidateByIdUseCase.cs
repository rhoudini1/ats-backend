using ATS.Application.Mapping;
using ATS.Contracts.Responses;
using ATS.Domain.Interfaces.Repositories;

namespace ATS.Application.UseCases.Candidate.GetById;

public class GetCandidateByIdUseCase : IGetCandidateByIdUseCase
{
    private readonly ICandidateRepository _candidateRepository;

    public GetCandidateByIdUseCase(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<CandidateResponse?> Execute(Guid id, CancellationToken token)
    {
        var candidate = await _candidateRepository.GetByIdAsync(id, token);

        if (candidate is null)
            return null;

        return candidate.MapToResponse();
    }
}
