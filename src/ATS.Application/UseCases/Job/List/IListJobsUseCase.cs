using ATS.Contracts.Requests;
using ATS.Contracts.Responses;

namespace ATS.Application.UseCases.Job.List;

public interface IListJobsUseCase
{
    Task<PagedResponse<JobResponse>> Execute(ListRequest request, CancellationToken token);
}
