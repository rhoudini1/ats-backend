using ATS.Application.Mapping;
using ATS.Contracts.Responses;
using ATS.Domain.Interfaces.Repositories;

namespace ATS.Application.UseCases.Job.GetById;

public class GetJobByIdUseCase : IGetJobByIdUseCase
{
    private readonly IJobRepository _jobRepository;

    public GetJobByIdUseCase(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<JobResponse?> Execute(Guid id, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetByIdAsync(id, cancellationToken);

        return job?.MapToResponse();
    }
}