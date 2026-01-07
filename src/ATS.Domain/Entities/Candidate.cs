namespace ATS.Domain.Entities;

public class Candidate
{
    public Guid Id { get; init; }

    public required string FullName { get; set; }

    public required string Email { get; set; }

    public string? ResumeUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
