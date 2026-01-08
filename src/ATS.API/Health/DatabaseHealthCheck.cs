using ATS.Domain.Exceptions.Base;
using ATS.Infrastructure.Data;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ATS.API.Health;

public class DatabaseHealthCheck : IHealthCheck
{
    public const string Name = "Database";

    private readonly AppDbContext _dbContext;
    private readonly ILogger<DatabaseHealthCheck> _logger;

    public DatabaseHealthCheck(AppDbContext dbContext, ILogger<DatabaseHealthCheck> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var canConnect = await _dbContext.Database.CanConnectAsync(cancellationToken);
            if (canConnect)
            {
                return HealthCheckResult.Healthy(ErrorMessages.DatabaseHealth.Healthy);
            }
            else
            {
                return HealthCheckResult.Unhealthy(ErrorMessages.DatabaseHealth.Unhealthy);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning(ErrorMessages.DatabaseHealth.Timeout);
            return HealthCheckResult.Unhealthy(ErrorMessages.DatabaseHealth.Timeout);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ErrorMessages.DatabaseHealth.Error);
            return HealthCheckResult.Unhealthy(ErrorMessages.DatabaseHealth.Error, ex);
        }
    }
}
