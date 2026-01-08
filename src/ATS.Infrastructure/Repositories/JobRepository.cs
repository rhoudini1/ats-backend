using ATS.Domain.Entities;
using ATS.Domain.Interfaces.Repositories;
using ATS.Infrastructure.Data;

namespace ATS.Infrastructure.Repositories;

public class JobRepository : BaseRepository<Job>, IJobRepository
{
    public JobRepository(AppDbContext context) : base(context) { }
}