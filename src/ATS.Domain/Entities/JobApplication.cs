using ATS.Domain.Enums;

namespace ATS.Domain.Entities;

public class JobApplication
{
    public Guid Id { get; init; }

    public required Guid CandidateId { get; set; }
    public required Guid JobId { get; set; }

    public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
    public EApplicationStatus Status { get; set; } = EApplicationStatus.Applied;

    // Snapshot properties
    public string CandidateName { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
}
