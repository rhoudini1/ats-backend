using ATS.Application.UseCases.Candidate.Register;
using Microsoft.Extensions.DependencyInjection;

namespace ATS.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRegisterCandidateUseCase, RegisterCandidateUseCase>();
    }
}
