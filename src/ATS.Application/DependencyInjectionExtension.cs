using ATS.Application.UseCases.Candidate.Delete;
using ATS.Application.UseCases.Candidate.GetById;
using ATS.Application.UseCases.Candidate.List;
using ATS.Application.UseCases.Candidate.ListApplications;
using ATS.Application.UseCases.Candidate.Register;
using ATS.Application.UseCases.Candidate.Update;
using ATS.Application.UseCases.Job.Delete;
using ATS.Application.UseCases.Job.GetById;
using ATS.Application.UseCases.Job.List;
using ATS.Application.UseCases.Job.ListApplications;
using ATS.Application.UseCases.Job.Register;
using ATS.Application.UseCases.Job.Update;
using ATS.Application.UseCases.JobApplication.Register;
using Microsoft.Extensions.DependencyInjection;

namespace ATS.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRegisterCandidateUseCase, RegisterCandidateUseCase>();
        services.AddScoped<IGetCandidateByIdUseCase, GetCandidateByIdUseCase>();
        services.AddScoped<IListCandidatesUseCase, ListCandidatesUseCase>();
        services.AddScoped<IUpdateCandidateUseCase, UpdateCandidateUseCase>();
        services.AddScoped<IDeleteCandidateByIdUseCase, DeleteCandidateByIdUseCase>();
        services.AddScoped<IListCandidateApplicationsUseCase, ListCandidateApplicationsUseCase>();

        services.AddScoped<IRegisterJobUseCase, RegisterJobUseCase>();
        services.AddScoped<IGetJobByIdUseCase, GetJobByIdUseCase>();
        services.AddScoped<IListJobsUseCase, ListJobsUseCase>();
        services.AddScoped<IUpdateJobUseCase, UpdateJobUseCase>();
        services.AddScoped<IDeleteJobByIdUseCase, DeleteJobByIdUseCase>();
        services.AddScoped<IListJobApplicationsUseCase, ListJobApplicationsUseCase>();

        services.AddScoped<IRegisterJobApplicationUseCase, RegisterJobApplicationUseCase>();
    }
}
