using ATS.Contracts.Requests;
using ATS.Contracts.Responses;

namespace ATS.Application.UseCases.Job.ListApplications;

public interface IListJobApplicationsUseCase
{
    Task<PagedResponse<JobApplicationResponse>> Execute(
        Guid jobId,
        ListRequest request,
        CancellationToken token);
}