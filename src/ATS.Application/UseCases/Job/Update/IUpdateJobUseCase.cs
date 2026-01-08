using ATS.Contracts.Requests;
using ATS.Contracts.Responses;

namespace ATS.Application.UseCases.Job.Update;

public interface IUpdateJobUseCase
{
    Task<JobResponse> Execute(Guid id, UpdateJobRequest request, CancellationToken token);
}
