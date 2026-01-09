using ATS.Domain.Entities;

namespace ATS.Domain.Interfaces.Repositories;

public interface IJobApplicationRepository : IBaseRepository<JobApplication>
{
    Task<(IEnumerable<JobApplication> Items, int TotalCount)> GetPagedByCandidateIdAsync(
        Guid candidateId, int page, int size, CancellationToken cancellationToken);

    Task<(IEnumerable<JobApplication> Items, int TotalCount)> GetPagedByJobIdAsync(
        Guid jobId, int page, int size, CancellationToken cancellationToken);

    Task<bool> AlreadyAppliedAsync(Guid candidateId, Guid jobId, CancellationToken cancellationToken);
}
