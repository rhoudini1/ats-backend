using ATS.Domain.Entities;

namespace ATS.Domain.Interfaces.Repositories;

public interface ICandidateRepository : IBaseRepository<Candidate>
{
    Task<Candidate?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}
