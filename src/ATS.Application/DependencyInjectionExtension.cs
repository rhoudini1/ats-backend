using ATS.Application.UseCases.Candidate.Delete;
using ATS.Application.UseCases.Candidate.GetById;
using ATS.Application.UseCases.Candidate.List;
using ATS.Application.UseCases.Candidate.Register;
using Microsoft.Extensions.DependencyInjection;

namespace ATS.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRegisterCandidateUseCase, RegisterCandidateUseCase>();
        services.AddScoped<IGetCandidateByIdUseCase, GetCandidateByIdUseCase>();
        services.AddScoped<IListCandidatesUseCase, ListCandidatesUseCase>();
        services.AddScoped<IDeleteCandidateByIdUseCase, DeleteCandidateByIdUseCase>();
    }
}
