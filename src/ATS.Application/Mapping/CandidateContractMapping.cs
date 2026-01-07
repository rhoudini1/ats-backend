using ATS.Contracts.Requests;
using ATS.Contracts.Responses;
using ATS.Domain.Entities;
using ATS.Domain.ValueObjects;

namespace ATS.Application.Mapping;

public static class CandidateContractMapping
{
    public static Candidate MapToCandidate(this RegisterCandidateRequest request)
    {
        return new Candidate
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            Email = Email.Create(request.Email),
            CreatedAt = DateTime.UtcNow
        };
    }

    public static RegisterCandidateResponse MapToResponse(this Candidate candidate)
    {
        return new RegisterCandidateResponse
        {
            Id = candidate.Id,
            FullName = candidate.FullName,
            Email = candidate.Email,
            CreatedAt = candidate.CreatedAt
        };
    }
}
