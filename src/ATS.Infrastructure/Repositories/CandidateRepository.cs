using ATS.Domain.Entities;
using ATS.Domain.Interfaces.Repositories;
using ATS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ATS.Infrastructure.Repositories;

public class CandidateRepository : BaseRepository<Candidate>, ICandidateRepository
{
    private readonly AppDbContext _context;

    public CandidateRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Candidate?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Candidates.FirstOrDefaultAsync(candidate => candidate.Email == email, cancellationToken);
    }
}
