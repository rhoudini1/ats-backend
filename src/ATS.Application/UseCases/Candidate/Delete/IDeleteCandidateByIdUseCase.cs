namespace ATS.Application.UseCases.Candidate.Delete;

public interface IDeleteCandidateByIdUseCase
{
    Task<bool> Execute(Guid candidateId, CancellationToken cancellationToken);
}
