using ATS.Contracts.Requests;
using ATS.Contracts.Responses;

namespace ATS.Application.UseCases.JobApplication.Register;

public interface IRegisterJobApplicationUseCase
{
    Task<JobApplicationResponse> Execute(
        RegisterJobApplicationRequest request, CancellationToken cancellationToken);
}
