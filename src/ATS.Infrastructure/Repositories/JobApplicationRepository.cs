using ATS.Domain.Entities;
using ATS.Domain.Interfaces.Repositories;
using ATS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ATS.Infrastructure.Repositories;

public class JobApplicationRepository : BaseRepository<JobApplication>, IJobApplicationRepository
{
    private readonly AppDbContext _context;

    public JobApplicationRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<(IEnumerable<JobApplication> Items, int TotalCount)> GetPagedByJobIdAsync(
        Guid jobId, int page, int size, CancellationToken token)
    {
        var query = _context.JobApplications
            .Where(jobApplication => jobApplication.JobId == jobId);

        int totalCount = await query.CountAsync(token);

        var items = await query
            .OrderByDescending(jobApplication => jobApplication.AppliedAt)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(token);

        return (items, totalCount);
    }

    public async Task<(IEnumerable<JobApplication> Items, int TotalCount)> GetPagedByCandidateIdAsync(
        Guid candidateId,
        int page,
        int size,
        CancellationToken token)
    {
        var query = _context.JobApplications
            .Where(jobApplication => jobApplication.CandidateId == candidateId);

        int totalCount = await query.CountAsync(token);

        var items = await query
            .OrderByDescending(jobApplication => jobApplication.AppliedAt)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(token);

        return (items, totalCount);
    }

    public async Task<bool> AlreadyAppliedAsync(Guid candidateId, Guid jobId, CancellationToken token)
    {
        return await _context.JobApplications
            .AnyAsync(jobApplication =>
                jobApplication.CandidateId == candidateId && jobApplication.JobId == jobId,
            token);
    }
}
