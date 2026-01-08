using ATS.Contracts.Requests;
using ATS.Contracts.Responses;
using ATS.Domain.Entities;
using ATS.Domain.Enums;

namespace ATS.Application.Mapping;

public static class JobContractMapping
{
    public static Job MapToJob(this RegisterJobRequest request)
    {
        return new Job
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Status = EJobStatus.Open,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static JobResponse MapToResponse(this Job job)
    {
        return new JobResponse
        {
            Id = job.Id,
            Title = job.Title,
            Description = job.Description,
            Status = job.Status.ToString(),
        };
    }
}