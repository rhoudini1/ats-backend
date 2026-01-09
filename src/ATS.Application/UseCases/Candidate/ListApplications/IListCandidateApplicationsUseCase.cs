using ATS.Contracts.Requests;
using ATS.Contracts.Responses;

namespace ATS.Application.UseCases.Candidate.ListApplications;

public interface IListCandidateApplicationsUseCase
{
    Task<PagedResponse<JobApplicationResponse>> Execute(
        Guid candidateId,
        ListRequest request,
        CancellationToken token);
}
