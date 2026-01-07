using ATS.Contracts.Requests;
using ATS.Contracts.Responses;
using ATS.Domain.Entities;

namespace ATS.Application.Mapping;

public static class CandidateContractMapping
{
    public static Candidate MapToCandidate(this RegisterCandidateRequest request)
    {
        return new Candidate
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            Email = request.Email,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static CandidateResponse MapToResponse(this Candidate candidate)
    {
        return new CandidateResponse
        {
            Id = candidate.Id,
            FullName = candidate.FullName,
            Email = candidate.Email,
            CreatedAt = candidate.CreatedAt
        };
    }
}
