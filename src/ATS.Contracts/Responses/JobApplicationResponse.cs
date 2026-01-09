namespace ATS.Contracts.Responses;

public record CandidateData(Guid Id, string Name);

public record JobData(Guid Id, string Title);

public class JobApplicationResponse
{
    public required Guid Id { get; set; }
    public required CandidateData Candidate { get; set; }
    public required JobData Job { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime AppliedAt { get; set; }
}
