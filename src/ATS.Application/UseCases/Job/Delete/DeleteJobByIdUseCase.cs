using ATS.Domain.Interfaces.Repositories;

namespace ATS.Application.UseCases.Job.Delete;

public class DeleteJobByIdUseCase : IDeleteJobByIdUseCase
{
    private readonly IJobRepository _jobRepository;

    public DeleteJobByIdUseCase(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<bool> Execute(Guid id, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetByIdAsync(id, cancellationToken);

        if (job is null)
            return false;

        return await _jobRepository.DeleteAsync(id, cancellationToken);
    }
}
