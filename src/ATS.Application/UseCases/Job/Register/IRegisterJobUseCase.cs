using ATS.Contracts.Requests;
using ATS.Contracts.Responses;

namespace ATS.Application.UseCases.Job.Register;

public interface IRegisterJobUseCase
{
    Task<JobResponse> Execute(RegisterJobRequest request, CancellationToken cancellationToken);
}