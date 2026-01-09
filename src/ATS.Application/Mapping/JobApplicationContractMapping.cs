using ATS.Contracts.Requests;
using ATS.Contracts.Responses;
using ATS.Domain.Entities;
using ATS.Domain.Enums;

namespace ATS.Application.Mapping;

public static class JobApplicationContractMapping
{
    public static JobApplication MapToJobApplication(this RegisterJobApplicationRequest request)
    {
        return new JobApplication
        {
            Id = Guid.NewGuid(),
            CandidateId = request.CandidateId,
            JobId = request.JobId,
        };
    }

    public static JobApplicationResponse MapToResponse(this JobApplication application)
    {
        return new JobApplicationResponse
        {
            Id = application.Id,
            Candidate = new CandidateData(application.CandidateId, application.CandidateName),
            Job = new JobData(application.JobId, application.JobTitle),
            Status = application.Status.ToString(),
            AppliedAt = application.AppliedAt,
        };
    }
}