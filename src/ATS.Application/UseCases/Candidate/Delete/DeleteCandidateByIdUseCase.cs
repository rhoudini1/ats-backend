
using ATS.Domain.Interfaces.Repositories;

namespace ATS.Application.UseCases.Candidate.Delete;

public class DeleteCandidateByIdUseCase : IDeleteCandidateByIdUseCase
{
    private readonly ICandidateRepository _candidateRepository;

    public DeleteCandidateByIdUseCase(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<bool> Execute(Guid id, CancellationToken cancellationToken)
    {
        var candidate = await _candidateRepository.GetByIdAsync(id, cancellationToken);

        if (candidate is null)
            return false;

        return await _candidateRepository.DeleteAsync(id, cancellationToken);
    }
}
