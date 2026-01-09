using ATS.Domain.Interfaces.Repositories;
using ATS.Infrastructure.Data;
using ATS.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ATS.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMongoDB(configuration.GetConnectionString("MongoDb"), databaseName: "ATS");
        });

        services.AddScoped<ICandidateRepository, CandidateRepository>();
        services.AddScoped<IJobRepository, JobRepository>();
        services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
    }
}
