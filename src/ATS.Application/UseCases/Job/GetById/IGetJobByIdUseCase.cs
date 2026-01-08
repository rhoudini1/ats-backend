using ATS.Contracts.Responses;

namespace ATS.Application.UseCases.Job.GetById;

public interface IGetJobByIdUseCase
{
    Task<JobResponse?> Execute(Guid id, CancellationToken cancellationToken);
}
