using Microsoft.AspNetCore.OutputCaching;

namespace ATS.API.Policies;

public sealed class CandidateApplicationsCachePolicy : IOutputCachePolicy
{
    public ValueTask CacheRequestAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        var id = context.HttpContext.Request.RouteValues["id"]?.ToString();

        if (!string.IsNullOrEmpty(id))
        {
            context.Tags.Add($"candidate-apps-{id}");
        }

        context.AllowCacheLookup = true;
        context.AllowCacheStorage = true;
        context.AllowLocking = true;

        return ValueTask.CompletedTask;
    }

    public ValueTask CacheRevalidationAsync(
        OutputCacheContext context, CancellationToken cancellation) => ValueTask.CompletedTask;

    public ValueTask ServeFromCacheAsync(
        OutputCacheContext context, CancellationToken cancellation) => ValueTask.CompletedTask;

    public ValueTask ServeResponseAsync(
        OutputCacheContext context, CancellationToken cancellation) => ValueTask.CompletedTask;
}
