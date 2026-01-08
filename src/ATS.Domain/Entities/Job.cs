using ATS.Domain.Enums;

namespace ATS.Domain.Entities;

public class Job
{
    public Guid Id { get; init; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public EJobStatus Status { get; set; } = EJobStatus.Open;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
