namespace ATS.Application.UseCases.Job.Delete;

public interface IDeleteJobByIdUseCase
{
    Task<bool> Execute(Guid id, CancellationToken cancellationToken);
}
